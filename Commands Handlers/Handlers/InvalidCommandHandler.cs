using VK_QueueBot.Enums;
using VK_QueueBot.Interfaces;
using VK_QueueBot.Models;
using VK_QueueBot.Providers;

namespace VK_QueueBot.Commands_Handlers.Handlers;
public class InvalidCommandHandler : ICommandHandler
{
    public Command CommandProp => Command.InvalidCommand;

    public bool Handle(Queue q, object[]? args = null)
    {
        MessagingProvider.SendMessage(q.Peer, Messages.InvalidCommand);
        return true;
    }
}

