using VK_QueueBot.Enums;
using VK_QueueBot.Models;

namespace VK_QueueBot.Interfaces;

public interface ICommandHandler
{
    public Command CommandProp { get; }
    public bool Handle(Queue q, object []? args = null);
}