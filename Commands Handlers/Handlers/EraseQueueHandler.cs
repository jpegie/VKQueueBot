using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VK_QueueBot.Enums;
using VK_QueueBot.Interfaces;
using VK_QueueBot.Models;
using VK_QueueBot.Providers;
using VkNet.Model;

namespace VK_QueueBot.Commands_Handlers.Handlers;
public class EraseQueueHandler : CommandHandlerForStartedQueue
{
    public override Command CommandProp => Command.EraseQueue;

    public override bool HandleStartedQueueCommand(Queue q, object[]? args = null)
    {
        q.ClearMembers();
        MessagingProvider.SendMessage(q.Peer, Messages.QueueClear);
        return true;
    }
}
