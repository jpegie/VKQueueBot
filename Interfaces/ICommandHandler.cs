namespace VK_QueueBot.Interfaces;

public interface ICommandHandler
{
    public long? Peer { get; set; }
    public bool Handle(long? peer);
}