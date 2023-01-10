using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VK_QueueBot.Commands_Handlers;
using VK_QueueBot.Commands_Handlers.Handlers;
using VK_QueueBot.CommandsHandlers;
using VK_QueueBot.Data;
using VK_QueueBot.Enums;
using VK_QueueBot.Extensions;
using VK_QueueBot.Providers;
using VkNet;
using VkNet.Model;
using VkNet.Model.Results.Notifications;

namespace VK_QueueBot.Models;
public static class InputMessageHandler
{
    private static VkApi? _api = null;
    public static void Init(VkApi api)
    {
        _api = api;
    }
    /// <summary>
    /// Обработчик сообщения - выбирает действие, которое нужно сделать для входной команды из message.Text
    /// </summary>
    public static void HandleMessage(Message message)
    {
        if (_api == null)
        {
            throw new NotImplementedException();
        }
       /* if (message.GetValidationStatus() == MessageValidationStatus.InvalidCommand)
        {
            MessagingProvider.SendMessage(message.PeerId, Messages.InvalidCommand);
            return;
        }*/

        var peer = message.PeerId;
        var processingQ = QueueProvider.InitQueue(peer);

        var currentCommand = DefineInputCommandByMessage(message);
        var commandArgs = DefineArgs(message, currentCommand);
        var handler = CommandsHandlersProvider.GetHandler(currentCommand);
        if (handler == null)
        {
            throw new NotImplementedException();
        }
        else
        {
            handler.Handle(processingQ, commandArgs);
        }
    }

    private static Command DefineInputCommandByMessage(Message message)
    {
        var firstSpacePos = message.Text.IndexOf(" ");
        var possibleCommandString = message.Text.Substring(0, firstSpacePos);
        var currentCommand = Commands.GetCommandByName(possibleCommandString);
        var currentQ = QueueProvider.InitQueue(message.PeerId);
        if (!currentQ.IsStarted)
        {
            currentCommand = Command.QueueIsNotStarted;
        }
        return currentCommand;
    }

    private static object[] DefineArgs(Message message, Command command)
    {
        var sender = _api!.Users.Get(new[] { (long)message.FromId! }).ElementAt(0);
        var args = new List<object>();

        //TODO: переписать дрянь под что-то более интересное
        if (command == Command.AddMember)
        {
            args.Add(sender);
        }

        if (command == Command.MoveToEnd)
        {
            args.Add(sender);
        }
        return args.ToArray();
    }
}

