namespace DeFUSE.Dispatcher;

public interface IReply
{
    Task ReplyError(int error);
    Task Reply();
}