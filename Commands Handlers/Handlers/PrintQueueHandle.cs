using VK_QueueBot.Enums;
using VK_QueueBot.Models;
using VK_QueueBot.Providers;

namespace VK_QueueBot.Commands_Handlers.Handlers;
public class PrintQueueHandle : CommandHandlerForStartedQueue
{
    public override Command CommandProp => Command.PrintQueue;

    public override bool HandleStartedQueueCommand(Queue q, object[]? args = null)
    {
        q.ClearMembers();
        MessagingProvider.SendMessage(q.Peer, Messages.QueueClear);
        return true;
    }
}

