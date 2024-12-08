using System.Runtime.InteropServices;
using DeFUSE.Core;
using DeFUSE.Core.Fuse;
using DeFUSE.Interop;
using DeFUSE.Interop.Native;
using DeFUSE.Utils;
using Microsoft.Extensions.Logging;
using Microsoft.Win32.SafeHandles;

namespace DeFUSE.Dispatcher;

public class FuseSession : IDisposable
{
    private readonly FuseContext _ctx;
    private readonly IFileSystem _fs;
    private readonly ILogger? _logger;
    private readonly FileStream _fileStream;
    private readonly SafeFileHandle _fileHandle;
    private byte[] _buffer;

    
    private const uint MaxWriteSize = 16 * 1024 * 1024;

    /// Size of the buffer for reading a request from the kernel. Since the kernel may send
    /// up to MAX_WRITE_SIZE bytes in a write request, we use that value plus some extra space.
    private const uint BufferSize = MaxWriteSize + 4096;
    
    
    public FuseSession(FuseContext ctx, IFileSystem fs,  ILogger? logger)
    {
        _ctx = ctx;
        _fs = fs;
        _logger = logger;
        _fileHandle = new SafeFileHandle(ctx.FileDescriptor, true);
        _fileStream = new FileStream(_fileHandle, FileAccess.ReadWrite);
        _buffer = new byte[BufferSize];  
    }
    
    public Task StartSession(CancellationToken ct =default)
    {

        var bufferSpan = _buffer.AsSpan();
        int minReadSize = Marshal.SizeOf<FuseInHeader>();
        while (!ct.IsCancellationRequested)
        {
            int read = _fileStream.ReadAtLeast(bufferSpan, minReadSize, false);
            if (read > 0)
            {
                var (ok, header, newOffset) = MemoryUtils.IterateUntil<FuseInHeader>(bufferSpan);
                if (!ok)
                {
                    _logger?.LogDebug($"Read {read} bytes from kernel but unable to decode it as FuseInHeader");
                    continue;
                }
                FuseRequest.CreateRequest(header, _fs)
                    .WithDataCallback(HandleResponse)
                    .WithErrorCallback(HandleErrorResponse)
                    .Send();
                

            }
        }

        return Task.CompletedTask;
    }

    public Task HandleResponse(ulong unique, byte[] data) 
    {
        return Task.CompletedTask;
    }

    public Task HandleErrorResponse(ulong unique)
    {
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _ctx.Dispose();
    }
}