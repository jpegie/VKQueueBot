using System.Runtime.CompilerServices;
using VK_QueueBot.Enums;
using VkNet.Model;

namespace VK_QueueBot.Extensions;
public static class Extensions
{
    private static ConditionalWeakTable<Message, ValidationStatus> _statuses = new ConditionalWeakTable<Message, ValidationStatus>();  
    private static Random rand = new Random();
    public static void Shuffle<T>(this IList<T> values)
    {
        for (int i = values.Count - 1; i > 0; i--)
        {
            int k = rand.Next(i + 1); //TODO: добавить нормальный шафл из ветки main
            T value = values[k];
            values[k] = values[i];
            values[i] = value;
        }
    }

    public static string CombinedName(this User usr)
    {
        return usr.FirstName + " " + usr.LastName;
    }
    public static bool MyEquals(this User usr, User comparingUser)
    {
        return usr.Id == comparingUser.Id;
    }
    public static MessageValidationStatus GetValidationStatus (this Message msg)
    {
        return _statuses.GetOrCreateValue(msg).Status;
    }
    public static void SetValidationStatus (this Message msg, MessageValidationStatus status)
    {
        _statuses.GetOrCreateValue(msg).Status = status;
    }
}

class ValidationStatus
{
    public MessageValidationStatus Status { get; set; }
}
