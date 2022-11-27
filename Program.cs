using VK_QueueBot;
using VK_QueueBot.Data;
using VK_QueueBot.Enums;
using VK_QueueBot.Extensions;
using VK_QueueBot.Models;
using VK_QueueBot.Providers;
using VkNet;
using VkNet.Model;

VkApi? api = null;
Task? closingQueueTask = null;
List<Queue> _queues = new List<Queue>();    

Setup();
GoWork();

/// <summary>
/// Инициализация полей, настройка модулей
/// </summary>
void Setup()
{
    api = new VkApi();
    MessagingProvider.Api = api;
}

/// <summary>
/// Основная работа
/// </summary>
void GoWork()
{
    while (true)
    {
        if (api!.IsAuthorized == false)
        {
            api.Authorize(new ApiAuthParams
            {
                AccessToken = AuthData.AccessToken
            });
        }
        var commands = MessagingProvider.GetCommandMessages();
        commands.ForEach(message =>
        {
            ProcessMessage(message);
        });
    }
}

/// <summary>
/// Обработчик сообщения - выбирает действие, которое нужно сделать для входной команды из message.Text
/// </summary>
void ProcessMessage(Message message)
{
    if(message.GetValidationStatus() == MessageValidationStatus.InvalidCommand)
    {
        MessagingProvider.SendMessage(message.PeerId, Messages.InvalidCommand);
        return;
    }

    var processingQ = _queues.Find(q => q.Peer == message.PeerId);
    var messageText = message.Text;
    var sender = api.Users.Get(new [] { (long) message.FromId! }).ElementAt(0);
    var peer = message.PeerId;

    if (messageText.StartsWith(Commands.Dict[Command.CreateQueue]))
    {
        if (processingQ == null)
        {
            processingQ = new Queue(peer);
            _queues.Add(processingQ);
        }

        if (processingQ.IsStarted)
        {
            MessagingProvider.SendMessage(peer, Messages.AlreadyWorking);
            return;
        }
        else
        {
            processingQ.Reset();
            processingQ.IsStarted = true;

            MessagingProvider.SendMessage(peer, Messages.QueueCreated);

            if (closingQueueTask == null || closingQueueTask.Status != TaskStatus.Running)
            {
                closingQueueTask = new Task(() => ClosingQueueMethod(processingQ));
                closingQueueTask.Start();   
            }
            return;
        }
    }
    else if (messageText.StartsWith(Commands.Dict[Command.Help]))
    {
        MessagingProvider.SendMessage(peer, Messages.Help);
        return;
    }

    if (processingQ != null && processingQ.IsStarted)
    {
        if (messageText.StartsWith(Commands.Dict[Command.EraseQueue]))
        {
            processingQ.ClearMembers();
            MessagingProvider.SendMessage(peer, Messages.QueueClear);
        }
        else if (messageText.StartsWith(Commands.Dict[Command.PrintQueue]))
        {
            MessagingProvider.SendMessage(peer, processingQ.ToString());
        }
        else if (messageText.StartsWith(Commands.Dict[Command.AddMember]))
        {
            var isAdded = processingQ.AddMember(sender);
            if (isAdded)
            {
                if (processingQ.IsShuffled)
                {
                    MessagingProvider.SendMessage(peer, String.Format(Messages.NewLastQueueMember, sender.CombinedName()));
                }
                else
                {
                    MessagingProvider.SendMessage(peer, String.Format(Messages.NewQueueMember, sender.CombinedName()));
                }
            }
        }
        else if (messageText.StartsWith(Commands.Dict[Command.RemoveMember]))
        {
            var isRemoved = processingQ.RemoveMember(sender);
            if (isRemoved)
            {
                MessagingProvider.SendMessage(peer, String.Format(Messages.RemoveQueueMember, sender.CombinedName()));
            }
        }
        else if (messageText.StartsWith(Commands.Dict[Command.MoveToEnd]))
        {
            var isMoved = processingQ.MoveToEnd(sender);
            if (isMoved)
            {
                MessagingProvider.SendMessage(peer, String.Format(Messages.MovedToEnd, sender.CombinedName()));
            }
        }
        else if (messageText.StartsWith(Commands.Dict[Command.Shuffle]))
        {
            processingQ.Shuffle();
            MessagingProvider.SendMessage(peer, Messages.QueueGenerated);
        }
        else if (messageText.StartsWith(Commands.Dict[Command.SetClosingQueueTime]))
        {
            var messagePart = messageText.Split();
            if (messagePart.Count() != 2)
            {
                MessagingProvider.SendMessage(peer, Messages.InvalidTime);
                return;
            }

            DateTime time; 
            if (!DateTime.TryParse(messagePart[1], out time))
            {
                MessagingProvider.SendMessage(peer, Messages.InvalidTime);
                return;
            }
            else
            {
                processingQ.ClosingTime = time;
                MessagingProvider.SendMessage(peer, String.Format(Messages.QueueClosingTimeSetTo, time.ToShortTimeString()));
            }
        }
        else if (messageText.StartsWith(Commands.Dict[Command.RemoveQueue]))
        {
            _queues.Remove(processingQ);
            MessagingProvider.SendMessage(peer, Messages.QueueRemoved);
        }
        else if (messageText.StartsWith(Commands.Dict[Command.GetClosingTime]))
        {
            if (processingQ.IsClosingTimeSet)
            {
                MessagingProvider.SendMessage(peer, String.Format(Messages.QueueClosingTime, processingQ.ClosingTime.ToShortTimeString()));
            }
            else
            {
                MessagingProvider.SendMessage(peer, Messages.QueueClosingTimeNotSet);
            }
        }
        else
        {
            MessagingProvider.SendMessage(peer, Messages.InvalidCommand);
        }
    }
    else
    {
        MessagingProvider.SendMessage(peer, Messages.FirstToStartQueue);
    }

}

/// <summary>
/// Решает, когда запустить рандомизацию собранной очереди, работает в таске
/// </summary>
void ClosingQueueMethod(Queue q)
{
    while (!q.IsShuffled && q.Peer != null)
    {
        if (q.IsClosingTimeSet && DateTime.Now > q.ClosingTime)
        {
            q.Shuffle();
            MessagingProvider.SendMessage(q.Peer, Messages.QueueClosed);
        } 
    }
}