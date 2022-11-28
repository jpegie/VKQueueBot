using VK_QueueBot.Enums;
using VK_QueueBot.Interfaces;

namespace VK_QueueBot.CommandsHandlers;

public static class CommandsHandlersStorage
{
    private static Dictionary<Command, ICommandHandler> _handlers = new Dictionary<Command, ICommandHandler>();
    public static void AddCommandWithHandler(Command command, ICommandHandler handler)
    {
        if (!_handlers.ContainsKey(command))
        {
            _handlers.Add(command, handler);
        }
    }
    public static ICommandHandler? GetHandler(Command command)
    {
        if (_handlers.ContainsKey(command))
        {
            return _handlers[command];
        }
        else
        {
            return null;
        }
    }
}





