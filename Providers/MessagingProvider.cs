using VK_QueueBot.Data;
using VK_QueueBot.Enums;
using VK_QueueBot.Extensions;
using VkNet;
using VkNet.Model;
using VkNet.Model.GroupUpdate;
using VkNet.Model.RequestParams;

namespace VK_QueueBot.Providers;
public static class MessagingProvider
{
    public static VkApi Api { get; set; }
    public static List<Message> GetCommandMessages()
    {
        var messages = new List<Message>();
        var server = Api.Groups.GetLongPollServer(Consts.BotGroupId);
        var pollHistory = new BotsLongPollHistoryResponse();
        try
        {
            pollHistory = Api.Groups.GetBotsLongPollHistory(new BotsLongPollHistoryParams
            {
                Server = server.Server,
                Ts = server.Ts,
                Key = server.Key,
                Wait = Consts.WaitTime
            });
        }
        catch(Exception)
        {
            return messages;
        }

        if (pollHistory?.Updates != null && pollHistory.Updates.Count() != 0)
        {
            messages = pollHistory.Updates
                .Where(update => update.Instance is MessageNew)
                .Select(update => (update.Instance as MessageNew)!.Message)
                .Where(message =>
                { 
                    var status = IsMessageValid(message);
                    message.SetValidationStatus(status); //TODO: нужно подумать над этим куском кода и лучше вынести его куда-то, а то класс парсит сообщения + скрытно валидирует их
                    return status == MessageValidationStatus.ValidCommand || status == MessageValidationStatus.InvalidCommand;
                })
                .ToList();
        }
        return messages;
    }

    private static MessageValidationStatus IsMessageValid(Message message)
    {
        if (message == null)
        {
            return MessageValidationStatus.NotCommand;
        }
        if (!message.Text.StartsWith(Consts.CommandFirstSymbol))
        {
            return MessageValidationStatus.NotCommand;
        }
        if (!Commands.Dict.Values.Contains(message.Text.Split()[0]))
        {
            return MessageValidationStatus.InvalidCommand;
        }
        return MessageValidationStatus.ValidCommand;
    }

    public static void SendMessage(long? peer, string message)
    {
        Api.Messages.Send(new MessagesSendParams
        {
            PeerId = peer,
            Message = message,
            RandomId = new Random().Next()
        });
    }
}
