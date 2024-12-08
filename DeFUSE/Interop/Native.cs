using System.Runtime.InteropServices;

namespace DeFUSE.Interop.Native;

public struct FuseAttr
{
    public ulong Ino;
    public ulong Size;
    public ulong Blocks;
    public ulong Atime;
    public ulong Mtime;
    public ulong Ctime;
    public uint ATimensec;
    public uint MTimensec;
    public uint CTimensec;
    public uint Mode;
    public uint Nlink;
    public uint Uid;
    public uint Gid;
    public uint Rdev;
    public uint Blksize;
    public uint Flags;
};

/*
 * The following structures are bit-for-bit compatible with the statx(2) ABI in
 * Linux.
 */
public struct FuseSxTime
{
    public long TvSec;
    public uint TvNSec;
    public int Reserved;
};

public struct FuseStatX
{
    public uint Mask;
    public uint Blksize;
    public ulong Attributes;
    public uint Nlink;
    public uint Uid;
    public uint Gid;
    public ushort Mode;
    public unsafe fixed ushort Spare0[1];
    public ulong Ino;
    public ulong Size;
    public ulong Blocks;
    public ulong AttributesMask;
    public FuseSxTime Atime;
    public FuseSxTime Btime;
    public FuseSxTime Ctime;
    public FuseSxTime Mtime;
    public uint RdevMajor;
    public uint RdevMinor;
    public uint DevMajor;
    public uint DevMinor;
    public unsafe fixed ulong Spare2[14];
};

public struct FuseKStatFs
{
    public ulong Blocks;
    public ulong BFree;
    public ulong BAvailable;
    public ulong Files;
    public ulong FFree;
    public uint BSize;
    public uint NameLength;
    public uint FrSize;
    public uint Padding;
    public unsafe fixed uint Spare[6];
};

public struct FuseFileLock
{
    public ulong Start;
    public ulong End;
    public uint Type;
    public uint Pid; /* tgid */
};

public enum FuseExtType
{
    /* Types 0..31 are reserved for fuse_secctx_header */
    FuseMaxNrSecCtx = 31,
    FuseExtGroups = 32
};

public enum FuseOpCode : uint
{
    FuseLookup = 1,
    FuseForget = 2, /* no reply */
    FuseGetAttr = 3,
    FuseSetAttr = 4,
    FuseReadlink = 5,
    FuseSymlink = 6,
    FuseMknod = 8,
    FuseMkDir = 9,
    FuseUnlink = 10,
    FuseRmdir = 11,
    FuseRename = 12,
    FuseLink = 13,
    FuseOpen = 14,
    FuseRead = 15,
    FuseWrite = 16,
    FuseStatFs = 17,
    FuseRelease = 18,
    FuseFsync = 20,
    FuseSetXAttr = 21,
    FuseGetXAttr = 22,
    FuseListXAttr = 23,
    FuseRemoveXAttr = 24,
    FuseFlush = 25,
    FuseInit = 26,
    FuseOpenDir = 27,
    FuseReaddir = 28,
    FuseReleaseDir = 29,
    FuseFSyncDir = 30,
    FuseGetLk = 31,
    FuseSetLk = 32,
    FuseSetLkw = 33,
    FuseAccess = 34,
    FuseCreate = 35,
    FuseInterrupt = 36,
    FuseBMap = 37,
    FuseDestroy = 38,
    FuseIoctl = 39,
    FusePoll = 40,
    FuseNotifyReply = 41,
    FuseBatchForget = 42,
    FuseFAllocate = 43,
    FuseReadDirPlus = 44,
    FuseRename2 = 45,
    FuseLSeek = 46,
    FuseCopyFileRange = 47,
    FuseSetupMapping = 48,
    FuseRemoveMapping = 49,
    FuseSyncFs = 50,
    FuseTmpFile = 51,
    FuseStatX = 52,

    /* CUSE specific operations */
    CuseInit = 4096,

    /* Reserved opcodes: helpful to detect structure endian-ness */
    CuseInitBSwapReserved = 1048576, /* CUSE_INIT << 8 */
    FuseInitBSwapReserved = 436207616, /* FUSE_INIT << 24 */
};

public enum FuseNotifyCode
{
    FuseNotifyPoll = 1,
    FuseNotifyInValInode = 2,
    FuseNotifyInValEntry = 3,
    FuseNotifyStore = 4,
    FuseNotifyRetrieve = 5,
    FuseNotifyDelete = 6,
    FuseNotifyResend = 7,
    FuseNotifyCodeMax
};

public struct FuseEntryOut
{
    public ulong NodeId; /* Inode ID */

    public ulong Generation; /* Inode generation: nodeid:gen must
                       be unique for the fs's lifetime */

    public ulong EntryValid; /* Cache timeout for the name */
    public ulong AttrValid; /* Cache timeout for the attributes */
    public uint EntryValidNsec;
    public uint AttrValidNsec;
    public FuseAttr Attr;
};

public struct FuseForgetIn
{
    public ulong NLookup;
};

public struct FuseForgetOne
{
    public ulong NodeId;
    public ulong NLookup;
};

public struct FuseBatchForgetIn
{
    public uint Count;
    public uint Dummy;
};

public struct FuseGetAttrIn
{
    public uint GetAttrFlags;
    public uint Dummy;
    public ulong Fh;
};

public struct FuseAttrOut
{
    public ulong AttrValid; /* Cache timeout for the attributes */
    public uint AttrValidNsec;
    public uint Dummy;
    public FuseAttr Attr;
};

public struct FuseStatXIn
{
    public uint GetAttrFlags;
    public uint Reserved;
    public ulong Fh;
    public uint SxFlags;
    public uint SxMask;
};

public struct FuseStatXOut
{
    public ulong AttrValid; /* Cache timeout for the attributes */
    public uint AttrValidNSec;
    public uint Flags;
    public unsafe fixed ulong Spare[2];
    public FuseStatX Stat;
};

public struct FuseMknodIn
{
    public uint Mode;
    public uint Rdev;
    public uint Umask;
    public uint Padding;
};

public struct FuseMkdirIn
{
    public uint Mode;
    public uint Umask;
};

public struct FuseRenameIn
{
    public ulong NewDir;
};

public struct FuseRename2In
{
    public ulong NewDir;
    public uint Flags;
    public uint Padding;
};

public struct FuseLinkIn
{
    public ulong OldNodeId;
};

public struct FuseSetAttrIn
{
    public uint Valid;
    public uint Padding;
    public ulong Fh;
    public ulong Size;
    public ulong LockOwner;
    public ulong Atime;
    public ulong Mtime;
    public ulong Ctime;
    public uint ATimeNSec;
    public uint MTimeNSec;
    public uint CTimeNSec;
    public uint Mode;
    public uint Unused4;
    public uint Uid;
    public uint Gid;
    public uint Unused5; //TODO fix using fixed
};

public struct FuseOpenIn
{
    public uint Flags;
    public uint OpenFlags; /* FUSE_OPEN_... */
};

public struct FuseCreateIn
{
    public uint Flags;
    public uint Mode;
    public uint Umask;
    public uint OpenFlags; /* FUSE_OPEN_... */
};

public struct FuseOpenOut
{
    public ulong Fh;
    public uint OpenFlags;
    public int BackingId;
};

public struct FuseReleaseIn
{
    public ulong Fh;
    public uint Flags;
    public uint ReleaseFlags;
    public ulong LockOwner;
};

public struct FuseFlushIn
{
    public ulong Fh;
    public uint Unused;
    public uint Padding;
    public ulong LockOwner;
};

public struct FuseReadIn
{
    public ulong Fh;
    public ulong Offset;
    public uint Size;
    public uint ReadFlags;
    public ulong LockOwner;
    public uint Flags;
    public uint Padding;
};

public struct FuseWriteIn
{
    public ulong Fh;
    public ulong Offset;
    public uint Size;
    public uint WriteFlags;
    public ulong LockOwner;
    public uint Flags;
    public uint Padding;
};

public struct FuseWriteOut
{
    public uint Size;
    public uint Padding;
};

public struct FuseStatFsOut
{
    public FuseKStatFs St;
};

public struct FuseFsyncIn
{
    public ulong Fh;
    public uint FsyncFlags;
    public uint Padding;
};

public struct FuseSetXAttrIn
{
    public uint Size;
    public uint Flags;
    public uint SetXAttrFlags;
    public uint Padding;
};

public struct FuseGetXattrIn
{
    public uint Size;
    public uint Padding;
};

public struct FuseGetXAttrOut
{
    public uint Size;
    public uint Padding;
};

public struct FuseLkIn
{
    public ulong Fh;
    public ulong Owner;
    public FuseFileLock Lk;
    public uint LkFlags;
    public uint Padding;
};

public struct FuseLkOut
{
    public FuseFileLock Lk;
};

public struct FuseAccessIn
{
    public uint Mask;
    public uint Padding;
};

[StructLayout(LayoutKind.Sequential)]
public struct FuseInitIn
{
    public uint Major;
    public uint Minor;
    public uint MaxReadAhead;
    public uint Flags;
    public uint Flags2;
    public unsafe fixed uint unused[11];
};

[StructLayout(LayoutKind.Sequential)]
public struct FuseInitOut
{
    public uint major;
    public uint minor;
    public uint max_readahead;
    public uint flags;
    public ushort max_background;
    public ushort congestion_threshold;
    public uint max_write;
    public uint time_gran;
    public ushort max_pages;
    public ushort map_alignment;
    public uint flags2;
    public uint max_stack_depth;
    public unsafe fixed uint unused[6];
};

public struct CuseInitIn
{
    public uint Major;
    public uint Minor;
    public uint Unused;
    public uint Flags;
};

public struct CuseInitOut
{
    public uint Major;
    public uint Minor;
    public uint Unused;
    public uint Flags;
    public uint MaxRead;
    public uint MaxWrite;
    public uint DevMajor; /* chardev major */
    public uint DevMinor; /* chardev minor */
    public unsafe fixed uint Spare[10];
};

public struct FuseInterruptIn
{
    public ulong Unique;
};

public struct FuseBMapIn
{
    public ulong Block;
    public uint BlockSize;
    public uint Padding;
};

public struct FuseBmMpOut
{
    public ulong Block;
};

public struct FuseIoctlIn
{
    public ulong Fh;
    public uint Flags;
    public uint Cmd;
    public ulong Arg;
    public uint InSize;
    public uint OutSize;
};

public struct FuseIoctlIovec
{
    public ulong Base;
    public ulong Len;
};

public struct FuseIoctlOut
{
    public int Result;
    public uint Flags;
    public uint InIovs;
    public uint OutIovs;
};

public struct FusePollIn
{
    public ulong Fh;
    public ulong Kh;
    public uint Flags;
    public uint Events;
};

public struct FusePollOut
{
    public uint Revents;
    public uint Padding;
};

public struct FuseNotifyPollWakeupOut
{
    public ulong Kh;
};

public struct FuseFAllocateIn
{
    public ulong Fh;
    public ulong Offset;
    public ulong Length;
    public uint Mode;
    public uint Padding;
};

/**
 * FUSE request unique ID flag
 *
 * Indicates whether this is a resend request. The receiver should handle this
 * request accordingly.
 */
[StructLayout(LayoutKind.Sequential)]
public struct FuseInHeader
{
    public uint Len;
    public uint OpCode;
    public ulong Unique;
    public ulong NodeID;
    public uint UId;
    public uint GId;
    public uint PId;
    public ushort TotalExtLen; /* length of extensions in 8byte units */
    public ushort Padding;
};

[StructLayout(LayoutKind.Sequential)]
public struct FuseOutHeader
{
    public uint Len;
    public int Error;
    public ulong Unique;
};

public struct FuseDirent
{
    public ulong Ino;
    public ulong Off;
    public uint NameLen;
    public uint Type;
    public char[] Name;
};

public struct FuseDirentPlus
{
    public FuseEntryOut EntryOut;
    public FuseDirent Dirent;
};

public struct FuseNotifyInvalInodeOut
{
    public ulong Ino;
    public long Off;
    public long Len;
};

public struct FuseNotifyInvalEntryOut
{
    public ulong Parent;
    public uint NameLen;
    public uint Flags;
};

public struct FuseNotifyDeleteOut
{
    public ulong Parent;
    public ulong Child;
    public uint NameLen;
    public uint Padding;
};

public struct FuseNotifyStoreOut
{
    public ulong NodeId;
    public ulong Offset;
    public uint Size;
    public uint Padding;
};

public struct FuseNotifyRetrieveOut
{
    public ulong NotifyUnique;
    public ulong NodeId;
    public ulong Offset;
    public uint Size;
    public uint Padding;
};

/* Matches the size of fuse_write_in */
public struct FuseNotifyRetrieveIn
{
    public ulong Dummy1;
    public ulong Offset;
    public uint Size;
    public uint Dummy2;
    public ulong Dummy3;
    public ulong Dummy4;
};

public struct FuseBackingMap
{
    public int Fd;
    public uint Flags;
    public ulong Padding;
};

public struct FuseLSeekIn
{
    public ulong Fh;
    public ulong Offset;
    public uint Whence;
    public uint Padding;
};

public struct FuseLSeekOut
{
    public ulong Offset;
};

public struct FuseCopyFileRangeIn
{
    public ulong FhIn;
    public ulong OffIn;
    public ulong NodeIdOut;
    public ulong FhOut;
    public ulong OffOut;
    public ulong Len;
    public ulong Flags;
};

public struct FuseSetupMappingIn
{
    /* An already open handle */
    public ulong Fh;

    /* Offset into the file to start the mapping */
    public ulong Foffset;

    /* Length of mapping required */
    public ulong Len;

    /* Flags, FUSE_SETUPMAPPING_FLAG_* */
    public ulong Flags;

    /* Offset in Memory Window */
    public ulong Moffset;
};

public struct FuseRemoveMappingIn
{
    /* number of fuse_removemapping_one follows */
    public uint Count;
};

public struct FuseRemoveMappingOne
{
    /* Offset into the dax window start the unmapping */
    public ulong MOffset;

    /* Length of mapping required */
    public ulong Len;
};

public struct FuseSyncFsIn
{
    public ulong Padding;
};

/*
 * For each security context, send fuse_secctx with size of security context
 * fuse_secctx will be followed by security context name and this in turn
 * will be followed by actual context label.
 * fuse_secctx, name, context
 */
public struct FuseSecCtx
{
    public uint Size;
    public uint Padding;
};

/*
 * Contains the information about how many fuse_secctx structures are being
 * sent and what's the total size of all security contexts (including
 * size of fuse_secctx_header).
 *
 */
public struct FuseSecCtxHeader
{
    public uint Size;
    public uint NrSecCtx;
};

/**
 * public struct fuse_ext_header - extension header
 * @size: total size of this extension including this header
 * @type: type of extension
 *
 * This is made compatible with fuse_secctx_header by using type values >
 * FUSE_MAX_NR_SECCTX
 */
public struct FuseExtHeader
{
    public uint Size;
    public uint Type;
};

/**
 * public struct fuse_supp_groups - Supplementary group extension
 * @nr_groups: number of supplementary groups
 * @groups: flexible array of group IDs
 */
public struct FuseSuppGroups
{
    public uint NrGroups;
    public uint[] Groups;
};