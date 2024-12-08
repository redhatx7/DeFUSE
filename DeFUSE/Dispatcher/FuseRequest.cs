using DeFUSE.Core;
using DeFUSE.Interop;
using DeFUSE.Interop.Native;


namespace DeFUSE.Dispatcher;

public class FuseRequest
{
    public IFileSystem FileSystem { get; private set; }
    public FuseInHeader Header { get; private set; }
    public Func<ulong, Task> OnError { get; private set; }
    
    public Func<ulong, byte[], Task> OnData { get; set; }
    
    public static FuseRequest CreateRequest(FuseInHeader header, IFileSystem fs)
    {
        return new FuseRequest()
        {
            Header = header,
            FileSystem = fs,
        };
    }

    public FuseRequest WithErrorCallback(Func<ulong, Task> callback)
    {
        OnError = callback;
        return this;
    }

    public FuseRequest WithDataCallback(Func<ulong, byte[], Task> data)
    {
        OnData = data;
        return this;
    }

    public void Send()
    {
        
    }
}

