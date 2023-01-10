using VK_QueueBot.Enums;
using VK_QueueBot.Models;
using VK_QueueBot.Providers;

namespace VK_QueueBot.Commands_Handlers.Handlers;
public class RemoveQueueHandler : CommandHandlerForStartedQueue
{
    public override Command CommandProp => Command.RemoveQueue;

    public override bool HandleStartedQueueCommand(Queue q, object[]? args = null)
    {
        QueueProvider.RemoveQueueByPeer(q.Peer);
        MessagingProvider.SendMessage(q.Peer, Messages.QueueRemoved);
        return true;
    }
}

