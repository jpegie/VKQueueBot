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
public class HelpHandler : ICommandHandler
{
    public Command CommandProp => Command.Help;

    public bool Handle(Queue q, object[]?args = null)
    {
        MessagingProvider.SendMessage(q.Peer, Messages.Help);
        return true;
    }
}
