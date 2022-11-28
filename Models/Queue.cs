using VK_QueueBot.Extensions;
using VkNet.Model;

namespace VK_QueueBot.Models;
public class Queue
{
    private object _editMembersLocker = new object();
    private DateTime _closingTime;
    private bool _shuffled = false;
    private bool _started = false;
    private bool _isClosingTimeSet = false;
    private long? _peer;
    private List<User> _members = new List<User>();

    public Queue(long? peer)
    {
        _peer = peer;
    }
    public long? Peer => _peer;
    public bool IsStarted
    {
        get => _started;
        set => _started = value;
    }
    public bool IsShuffled => _shuffled;
    public bool IsClosingTimeSet => _isClosingTimeSet;
    public DateTime ClosingTime
    {
        get => _closingTime;
        set
        {
            _closingTime = value;
            _isClosingTimeSet = true;
        }
    }
    public List<User> Members => _members;
    public void Reset()
    {
        _shuffled = false;
        _started = false;
        _isClosingTimeSet = false;
        _members.Clear();
    }
    public bool AddMember(User member)
    {
        lock (_editMembersLocker)
        {
            if (_members.Find(m => m.MyEquals(member)) == null)
            {
                _members.Add(member);
                return true;
            }
            return false;
        }
    }
    public bool RemoveMember(User member)
    {
        lock (_editMembersLocker)
        {
            var removingMember = Members.Find(m => m.MyEquals(member));
            if (removingMember != null)
            {
                _members.Remove(removingMember);
                return true;
            }
            return false;
        }
    }
    public void ClearMembers()
    {
        _members.Clear();
    }
    public bool MoveToEnd(User member)
    {
        var result = false;

        lock (_editMembersLocker)
        {
            if (RemoveMember(member))
            {
                result = AddMember(member);
            }
            return result;
        }
    }
    public void Shuffle()
    {
        lock (_editMembersLocker)
        {
            _members.Shuffle();
            _shuffled = true;
        }
    }
    public override string ToString()
    {
        var memberI = 1;
        if (Members.Count == 0)
        {
            return Messages.QueueEmpty;
        }
        else
        {
            return String.Join("\n", Members.Select(member => $"{memberI++}. {member.CombinedName()} "));
        }
    }
}


