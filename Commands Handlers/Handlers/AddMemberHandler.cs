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
public class AddMemberHandler : CommandHandlerForStartedQueue
{
    public override Command CommandProp => Command.AddMember;

    public override bool HandleStartedQueueCommand(Queue q, object[]? args)
    {
        var userPosInArgs = GetUserPosInArgs(args);
        if (IsArgsEmpty(args) || userPosInArgs == -1)
        {
            return false;
        }
        //изначально было 
        var sender = args![userPosInArgs] as User;
        var isAdded = q.AddMember(sender!);
        if (isAdded)
        {
            if (q.IsShuffled)
            {
                MessagingProvider.SendMessage(q.Peer, String.Format(Messages.NewLastQueueMember, sender!.CombinedName()));
            }
            else
            {
                MessagingProvider.SendMessage(q.Peer, String.Format(Messages.NewQueueMember, sender!.CombinedName()));
            }
        }
        return true;
    }
}
