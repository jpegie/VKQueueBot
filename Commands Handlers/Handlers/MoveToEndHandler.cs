using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VK_QueueBot.Enums;
using VK_QueueBot.Extensions;
using VK_QueueBot.Models;
using VK_QueueBot.Providers;
using VkNet.Enums.SafetyEnums;
using VkNet.Model;

namespace VK_QueueBot.Commands_Handlers.Handlers;
public class MoveToEndHandler : CommandHandlerForStartedQueue
{
    public override Command CommandProp => Command.MoveToEnd;

    public override bool HandleStartedQueueCommand(Queue q, object[]? args)
    {
        var userPosInArgs = GetUserPosInArgs(args);
        if (userPosInArgs == -1)
        {
            return false;
        }
        var sender = args![userPosInArgs] as User;
        var isMoved = q.MoveToEnd(sender!);
        if (isMoved)
        {
            MessagingProvider.SendMessage(q.Peer, String.Format(Messages.MovedToEnd, sender!.CombinedName()));
        }
        return true;
    }
}
