using VK_QueueBot.Models;

namespace VK_QueueBot.Providers;

public static class QueueProvider
{
    private static List<Queue> _queues = new List<Queue>();
    public static Queue? GetQueueByPeer(long? peer)
    {
        return _queues.Find(q => q.Peer == peer);       
    }

    public static void RemoveQueueByPeer(long? peer)
    {
        var removingQ = _queues.Find(q => q.Peer == peer);
        if (removingQ == null)
        {
            return;
        }
        else
        {
            _queues.Remove(removingQ);
        }
    }

    public static void AddQueue(Queue addingQ)
    {
        if (_queues.Find(q => addingQ.Peer == q.Peer) == null)
        {
            _queues.Add(addingQ);
        }
    }
    public static void AddQueue(long? peer)
    {
        if (_queues.Find(q => q.Peer == peer) == null)
        {
            _queues.Add(new Queue(peer));
        }
    }

    public static Queue InitQueue(long? peer)
    {
        AddQueue(peer);
        return GetQueueByPeer(peer)!;
    }

}