using VK_QueueBot.Enums;

namespace VK_QueueBot.Data;
public static class Commands
{
    public static Dictionary<Command, string> Dict = new Dictionary<Command, string>
    {
        { Command.SetClosingQueueTime, "/задатьвремя" },
        { Command.GetClosingTime, "/время" },
        { Command.Help, "/помощь" },
        { Command.CreateQueue, "/создать" },
        { Command.RemoveQueue, "/удалить" },
        { Command.Shuffle, "/перемешать" },
        { Command.EraseQueue, "/очистить" },
        { Command.AddMember, "/записаться" },
        { Command.RemoveMember, "/выйти" },
        { Command.MoveToEnd, "/вконец" },
        { Command.PrintQueue, "/очередь" }
    };
}

