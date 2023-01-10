using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VK_QueueBot.Enums;
using VK_QueueBot.Extensions;
using VK_QueueBot.Models;
using VK_QueueBot.Providers;
using VkNet.Model;

namespace VK_QueueBot.Commands_Handlers.Handlers;
public class RemoveMemberHandler : CommandHandlerForStartedQueue
{
    public override Command CommandProp => Command.RemoveMember;

    public override bool HandleStartedQueueCommand(Queue q, object[]? args)
    {
        var userPosInArgs = GetUserPosInArgs(args);
        if (userPosInArgs == -1)
        {
            return false;
        }
        var sender = args![userPosInArgs] as User;
        var isRemoved = q.RemoveMember(sender!);
        if (isRemoved)
        {
            MessagingProvider.SendMessage(q.Peer, String.Format(Messages.RemoveQueueMember, sender!.CombinedName()));
        }
        return true;
    }
}

