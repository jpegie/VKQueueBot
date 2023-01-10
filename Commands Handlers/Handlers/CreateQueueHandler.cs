using VK_QueueBot.Enums;
using VK_QueueBot.Interfaces;
using VK_QueueBot.Models;
using VK_QueueBot.Providers;

namespace VK_QueueBot.Commands_Handlers.Handlers;
public class CreateQueueHandler : ICommandHandler
{
    public Command CommandProp => Command.CreateQueue;

    public bool Handle(Queue q, object[]? args = null)
    {
        if (q.IsStarted)
        {
            MessagingProvider.SendMessage(q.Peer, Messages.AlreadyWorking);
        }
        else
        {
            q.Reset();
            q.IsStarted = true;

            MessagingProvider.SendMessage(q.Peer, Messages.QueueCreated);
            q.InitClosingQueueTaskIfNotStarted();
        }
        return true;
    }
}
