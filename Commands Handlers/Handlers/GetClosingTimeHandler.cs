using VK_QueueBot.Enums;
using VK_QueueBot.Models;
using VK_QueueBot.Providers;

namespace VK_QueueBot.Commands_Handlers.Handlers;
public class GetClosingTimeHandler : CommandHandlerForStartedQueue
{
    public override Command CommandProp => Command.GetClosingTime;

    public override bool HandleStartedQueueCommand(Queue q, object[]? args = null)
    {
        if (q.IsClosingTimeSet)
        {
            MessagingProvider.SendMessage(q.Peer, String.Format(Messages.QueueClosingTime, q.ClosingTime.ToShortTimeString()));
        }
        else
        {
            MessagingProvider.SendMessage(q.Peer, Messages.QueueClosingTimeNotSet);
        }
        return true;
    }
}
