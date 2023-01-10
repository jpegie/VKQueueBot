using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VK_QueueBot.Enums;
using VK_QueueBot.Models;
using VK_QueueBot.Providers;
using VkNet.Model;

namespace VK_QueueBot.Commands_Handlers.Handlers;
public class SetClosingQueueTimeHandler : CommandHandlerForStartedQueue
{
    public override Command CommandProp => Command.SetClosingQueueTime;

    public override bool HandleStartedQueueCommand(Queue q, object[]? args)
    {
        if (IsArgsEmpty(args))
        {
            return false;
        }
        if (args![0].GetType() != typeof(string))
        {
            return false;
        }

        var messageText = (string) args[0];
        var messagePart = messageText.Split();
        if (messagePart.Count() != 2)
        {
            MessagingProvider.SendMessage(q.Peer, Messages.InvalidTime);
        }
        else
        {
            DateTime time;
            if (!DateTime.TryParse(messagePart[1], out time))
            {
                MessagingProvider.SendMessage(q.Peer, Messages.InvalidTime);
            }
            else
            {
                q.ClosingTime = time;
                MessagingProvider.SendMessage(q.Peer, String.Format(Messages.QueueClosingTimeSetTo, time.ToShortTimeString()));
            }
        }
        return true;
    }
}
