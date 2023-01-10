using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VK_QueueBot.Enums;
using VK_QueueBot.Models;
using VK_QueueBot.Providers;
using VkNet.Model;

namespace VK_QueueBot.Commands_Handlers.Handlers;
public class ShuffleHandler : CommandHandlerForStartedQueue
{
    public override Command CommandProp => Command.Shuffle;

    public override bool HandleStartedQueueCommand(Queue q, object[]? args = null)
    {
        q.Shuffle();
        MessagingProvider.SendMessage(q.Peer, Messages.QueueGenerated);
        return true;
    }
}
