using VK_QueueBot.Enums;
using VK_QueueBot.Interfaces;

namespace VK_QueueBot.CommandsHandlers;

public static class CommandsHandlersProvider
{
    private static List<ICommandHandler> _handlers = new List<ICommandHandler>();
    public static void AddCommandWithHandler(ICommandHandler handler)
    {
        if (_handlers.Find(h => h.CommandProp == handler.CommandProp) == null)
        {
            _handlers.Add(handler);
        }
    }
    public static ICommandHandler? GetHandler(Command command)
    {
        return _handlers.Find(h => h.CommandProp == command);
    }
}





