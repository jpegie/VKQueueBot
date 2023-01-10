using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VK_QueueBot.Enums;
using VK_QueueBot.Interfaces;
using VK_QueueBot.Models;
using VkNet.Model;

namespace VK_QueueBot.Commands_Handlers;
public abstract class CommandHandlerForStartedQueue : ICommandHandler
{
    public abstract Command CommandProp { get; }

    public bool Handle(Queue q, object[]? args = null)
    {
        if (q != null && q.IsStarted)
        {
            return HandleStartedQueueCommand(q, args);
        }
        return false;
    }

    public bool IsArgsEmpty(object[]? args)
    {
        return args == null || args.Length == 0;
    }
   /* public bool IsArgsIncludeUser(object[]?args)
    {
        return args != null && args[0].GetType() == typeof(User);
    }*/
   /// <summary>
   /// Возвращает -1, если юзера нет в аргументах
   /// </summary>
   /// <param name="args"></param>
   /// <returns></returns>
    public int GetUserPosInArgs(object[]?args)
    {
        var res = -1;

        if (args == null || args.Length == 0)
        {
            return res;
        }

        for (int i = 0; i < args.Length; ++i)
        {
            if (args[i] is User)
            {
                res = i;
                break;
            }
        }
        return res;
    }
    public abstract bool HandleStartedQueueCommand(Queue q, object[]? args);
}
