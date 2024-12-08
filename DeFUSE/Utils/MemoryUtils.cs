using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using DeFUSE.Interop;
using DeFUSE.Interop.Native;

namespace DeFUSE.Utils;

public class MemoryUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static (bool Ok, T Target, int newOffset) IterateUntil<T>(ReadOnlySpan<byte> buffer) where T : unmanaged
    {
        bool ok = MemoryMarshal.TryRead(buffer, out T target);
        if (ok)
        {
            return (true, target, Unsafe.SizeOf<T>());
        }

        return (false, default, 0 );
    }
    
    public static Span<byte> SerializeStruct<T>(T value) where T : struct
    {
        int len = Marshal.SizeOf<T>();
        Span<byte> buffer = new byte[len];
        MemoryMarshal.Write(buffer, in value);
        return buffer;
    }

    public static byte[] SerializeResponse<T>(ulong requestId, int errno, T reply) where T: struct 
    {
        int len = Unsafe.SizeOf<FuseOutHeader>() + Unsafe.SizeOf<T>();
        var fuseOutHeader = new FuseOutHeader()
        {
            Len = (uint)len,
            Error = 0,
            Unique = requestId
        };
        var header = SerializeStruct(fuseOutHeader);
        var replyBuffer = SerializeStruct(reply);
        byte[] response = [..header.ToArray(), ..replyBuffer.ToArray()];
        
        return response;
    }
}