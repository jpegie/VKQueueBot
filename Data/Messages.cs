using VK_QueueBot.Data;
using VK_QueueBot.Enums;

namespace VK_QueueBot;
public static class Messages
{
    public const string QueueCreated = "Очередь создана";
    public const string QueueRemoved = "Очередь удалена";
    public static string Help = @$"✨ Создать очередь: <{Commands.Dict[Command.CreateQueue]}>" + "\n" +
                                @$"✨ Удалить очередь: <{Commands.Dict[Command.RemoveQueue]}>" + "\n" +
                                @$"✨ Очистить участников очереди: <{Commands.Dict[Command.EraseQueue]}>" + "\n" +
                                $@"✨ Задать время для закрытия записи: <{Commands.Dict[Command.SetClosingQueueTime]} HH:MM>" + "\n" +
                                $@"✨ Узнать время закрытия записи: <{Commands.Dict[Command.GetClosingTime]} HH:MM>" + "\n" +
                                "\n" +
                                @$"✨ Записаться: <{Commands.Dict[Command.AddMember]}>" + "\n" +
                                @$"✨ Выйти из очереди: <{Commands.Dict[Command.RemoveMember]}>" + "\n" +
                                @$"✨ Сметиться в конец очереди: <{Commands.Dict[Command.MoveToEnd]}>" + "\n" +
                                @$"✨ Перемешать: <{Commands.Dict[Command.Shuffle]}>";

    public const string NewQueueMember = "{0} добавлен(-а) в очередь";
    public const string NewLastQueueMember = "{0} добавлен(-а) в конец очереди";
    public const string RemoveQueueMember = "{0} исключен(-а) из очереди";
    public const string QueueGenerated = "Очередь перемешана";
    public const string QueueRegenerated = "Очередь пересобрана";
    public const string AlreadyInQueue = "{0} уже в очереди";
    public const string MovedToEnd = "{0} перемещен(-а) в конец";
    public const string NotInQueue = "{0} нет в очереди";
    public const string CurrentQueue = "Текущая очередь: {0}\n{1}";
    public const string QueueEmpty = "Очередь пустая";
    public const string QueueClear = "Очередь очищена";
    public const string InvalidTime = "Неправильно задано время (команда должна быть в виде </время 12:44>)";
    public const string QueueClosed = "Очередь сгенерирована, остальные участники будут записаны в конец";
    public static string AlreadyWorking = $"Чтобы создать новую очередь, завершите старую с помощью команды <{Commands.Dict[Command.RemoveQueue]}>";
    public static string InvalidCommand = $"Я не знаю такую команду\nДля справки введите </помощь>";
    public static string FirstToStartQueue = $"Сначала создайте очередь командой <{Commands.Dict[Command.CreateQueue]}>";
    public const string QueueClosingTimeSetTo = "Время закрытия очереди назначено на: {0}";
    public const string QueueClosingTime = "Время закрытия очереди: {0}";
    public const string QueueClosingTimeNotSet = "Время закрытия очереди не задано";

}





