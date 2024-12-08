using System.Buffers;
using System.Text;

namespace DeFUSE.Dispatcher;

public static class FuseReply
{
    public record struct EmptyReply(ulong Unique, Func<ReadOnlyMemory<byte>, Task> Callback) : IReply
    {
        public Task ReplyError(int error)
        {
            var errorData = BitConverter.GetBytes(error);
            return Callback(new ReadOnlyMemory<byte>(errorData));
        }

        public Task Reply()
        {
            return Callback(ReadOnlyMemory<byte>.Empty);
        }

        public static EmptyReply CreateNewReply(ulong unique, Func<ReadOnlyMemory<byte>, Task> callback)
        {
            return new EmptyReply(unique, callback);
        }
    }

    public record struct AttrReply(TimeSpan Ttl, string FileAttr) : IReply
    {
        internal EmptyReply _emptyReply;

        internal static AttrReply CreateNewReply(ulong unique, Func<ReadOnlyMemory<byte>, Task> callback)
        {
            return new AttrReply()
            {
                _emptyReply = new EmptyReply(unique, callback)
            };
        }

        public void WithAttr(TimeSpan ttl, string fileAttr)
        {
            Ttl = ttl;
            FileAttr = fileAttr;
        }

        public Task ReplyError(int error)
        {
            return _emptyReply.ReplyError(error);
        }

        public Task Reply()
        {
            var data = Encoding.UTF8.GetBytes($"{Ttl.Ticks}:{FileAttr}");
            return _emptyReply.Callback(new ReadOnlyMemory<byte>(data));
        }
    }

    public record struct CreateReply(TimeSpan Ttl, string FileAttr, ulong Generation, ulong FileHandle, uint Flags) : IReply
    {
        public Task ReplyError(int error)
        {
            var errorData = BitConverter.GetBytes(error);
            return Task.FromResult(new ReadOnlyMemory<byte>(errorData));
        }

        public Task Reply()
        {
            var data = Encoding.UTF8.GetBytes($"{Ttl.Ticks}:{FileAttr}:{Generation}:{FileHandle}:{Flags}");
            return Task.FromResult(new ReadOnlyMemory<byte>(data));
        }

        public static CreateReply CreateNewReply(TimeSpan ttl, string fileAttr, ulong generation, ulong fileHandle, uint flags)
        {
            return new CreateReply(ttl, fileAttr, generation, fileHandle, flags);
        }
    }

    public record struct StatfsReply(ulong Blocks, ulong BFree, ulong BAvail, ulong Files, ulong FFree, uint BSize, uint NameLen, uint FrSize) : IReply
    {
        public Task ReplyError(int error)
        {
            var errorData = BitConverter.GetBytes(error);
            return Task.FromResult(new ReadOnlyMemory<byte>(errorData));
        }

        public Task Reply()
        {
            var data = Encoding.UTF8.GetBytes($"{Blocks}:{BFree}:{BAvail}:{Files}:{FFree}:{BSize}:{NameLen}:{FrSize}");
            return Task.FromResult(new ReadOnlyMemory<byte>(data));
        }

        public static StatfsReply CreateNewReply(ulong blocks, ulong bFree, ulong bAvail, ulong files, ulong fFree, uint bSize, uint nameLen, uint frSize)
        {
            return new StatfsReply(blocks, bFree, bAvail, files, fFree, bSize, nameLen, frSize);
        }
    }

    public record struct WriteReply(uint Size) : IReply
    {
        public Task ReplyError(int error)
        {
            var errorData = BitConverter.GetBytes(error);
            return Task.FromResult(new ReadOnlyMemory<byte>(errorData));
        }

        public Task Reply()
        {
            var data = BitConverter.GetBytes(Size);
            return Task.FromResult(new ReadOnlyMemory<byte>(data));
        }

        public static WriteReply CreateNewReply(uint size)
        {
            return new WriteReply(size);
        }
    }

    public record struct EntryReply(TimeSpan Ttl, string FileAttr) : IReply
    {
        public Task ReplyError(int error)
        {
            var errorData = BitConverter.GetBytes(error);
            return Task.FromResult(new ReadOnlyMemory<byte>(errorData));
        }

        public Task Reply()
        {
            var data = Encoding.UTF8.GetBytes($"{Ttl.Ticks}:{FileAttr}");
            return Task.FromResult(new ReadOnlyMemory<byte>(data));
        }

        public static EntryReply CreateNewReply(TimeSpan ttl, string fileAttr)
        {
            return new EntryReply(ttl, fileAttr);
        }
    }

    public record struct OpenReply(ulong FileHandle, int Flags) : IReply
    {
        public Task ReplyError(int error)
        {
            var errorData = BitConverter.GetBytes(error);
            return Task.FromResult(new ReadOnlyMemory<byte>(errorData));
        }

        public Task Reply()
        {
            var data = Encoding.UTF8.GetBytes($"{FileHandle}:{Flags}");
            return Task.FromResult(new ReadOnlyMemory<byte>(data));
        }

        public static OpenReply CreateNewReply(ulong fileHandle, int flags)
        {
            return new OpenReply(fileHandle, flags);
        }
    }

    public record struct BmapReply(ulong Block) : IReply
    {
        public Task ReplyError(int error)
        {
            var errorData = BitConverter.GetBytes(error);
            return Task.FromResult(new ReadOnlyMemory<byte>(errorData));
        }

        public Task Reply()
        {
            var data = BitConverter.GetBytes(Block);
            return Task.FromResult(new ReadOnlyMemory<byte>(data));
        }

        public static BmapReply CreateNewReply(ulong block)
        {
            return new BmapReply(block);
        }
    }

    public record struct IoctlReply(int Result, ReadOnlySequence<byte> Data) : IReply
    {
        public Task ReplyError(int error)
        {
            var errorData = BitConverter.GetBytes(error);
            return Task.FromResult(new ReadOnlyMemory<byte>(errorData));
        }

        public Task Reply()
        {
            return Task.FromResult(Data.ToArray());
        }

        public static IoctlReply CreateNewReply(int result, ReadOnlySequence<byte> data)
        {
            return new IoctlReply(result, data);
        }
    }

    public record struct LSeekReply(long Offset) : IReply
    {
        public Task ReplyError(int error)
        {
            var errorData = BitConverter.GetBytes(error);
            return Task.FromResult(new ReadOnlyMemory<byte>(errorData));
        }

        public Task Reply()
        {
            var data = BitConverter.GetBytes(Offset);
            return Task.FromResult(new ReadOnlyMemory<byte>(data));
        }

        public static LSeekReply CreateNewReply(long offset)
        {
            return new LSeekReply(offset);
        }
    }

    public record struct XAttrReply(int Size) : IReply
    {
        public Task ReplyError(int error)
        {
            var errorData = BitConverter.GetBytes(error);
            return Task.FromResult(new ReadOnlyMemory<byte>(errorData));
        }

        public Task Reply()
        {
            var data = BitConverter.GetBytes(Size);
            return Task.FromResult(new ReadOnlyMemory<byte>(data));
        }

        public static XAttrReply CreateNewReply(int size)
        {
            return new XAttrReply(size);
        }
    }

    public record struct LockReply(ulong Start, ulong End, int Type, int PId) : IReply
    {
        public Task ReplyError(int error)
        {
            var errorData = BitConverter.GetBytes(error);
            return Task.FromResult(new ReadOnlyMemory<byte>(errorData));
        }

        public Task Reply()
        {
            var data = Encoding.UTF8.GetBytes($"{Start}:{End}:{Type}:{PId}");
            return Task.FromResult(new ReadOnlyMemory<byte>(data));
        }

        public static LockReply CreateNewReply(ulong start, ulong end, int type, int pId)
        {
            return new LockReply(start, end, type, pId);
        }
    }
}