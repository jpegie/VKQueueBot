using VK_QueueBot;
using VK_QueueBot.Commands_Handlers.Handlers;
using VK_QueueBot.CommandsHandlers;
using VK_QueueBot.Data;
using VK_QueueBot.Enums;
using VK_QueueBot.Extensions;
using VK_QueueBot.Models;
using VK_QueueBot.Providers;
using VkNet;
using VkNet.Model;

VkApi? api = null;   

Init();
GoWork();

/// <summary>
/// Инициализация полей, настройка модулей
/// </summary>
void Init()
{
    api = new VkApi();
    MessagingProvider.Api = api;
    InputMessageHandler.Init(api);
    FillHandlersProvider();
}

/// <summary>
/// Основная работа
/// </summary>
void GoWork()
{
    while (true)
    {
        if (api!.IsAuthorized == false)
        {
            api.Authorize(new ApiAuthParams
            {
               // AccessToken = AuthData.AccessToken
            });
        }
        var commands = MessagingProvider.GetCommandMessages();
        commands.ForEach(message =>
        {
            InputMessageHandler.HandleMessage(message);
        });
    }
}

void FillHandlersProvider()
{
    CommandsHandlersProvider.AddCommandWithHandler(new AddMemberHandler());
    CommandsHandlersProvider.AddCommandWithHandler(new CreateQueueHandler());
    CommandsHandlersProvider.AddCommandWithHandler(new EraseQueueHandler());
    CommandsHandlersProvider.AddCommandWithHandler(new GetClosingTimeHandler());
    CommandsHandlersProvider.AddCommandWithHandler(new HelpHandler());
    CommandsHandlersProvider.AddCommandWithHandler(new MoveToEndHandler());
    CommandsHandlersProvider.AddCommandWithHandler(new PrintQueueHandle());
    CommandsHandlersProvider.AddCommandWithHandler(new RemoveMemberHandler());
    CommandsHandlersProvider.AddCommandWithHandler(new RemoveQueueHandler());
    CommandsHandlersProvider.AddCommandWithHandler(new SetClosingQueueTimeHandler());
    CommandsHandlersProvider.AddCommandWithHandler(new ShuffleHandler());
    CommandsHandlersProvider.AddCommandWithHandler(new InvalidCommandHandler());
    CommandsHandlersProvider.AddCommandWithHandler(new NotStartedQueueHandler());
}
