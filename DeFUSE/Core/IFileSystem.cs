using DeFUSE.Interop;
using DeFUSE.Interop.Native;

namespace DeFUSE.Core;

public interface IFileSystem
{
    
    void Forget(ulong nodeId, ulong nLookup);

    void Forget(FuseInHeader header, string name, FuseEntryOut entryOut);

    void GetAttr();
    
    void SetAttr();

    void Mknod();

    void Mkdir();

    void Unlink();

    void Rmdir();

    void Rename();

    void Link();

    void Symlink();

    void ReadLink();

    void Access();

    void GetXAttr();

    void ListXAttr();

    void SetXAttr();

    void RemoveXAttr();

    void Create();

    void Open();

    void Read();

    void LSeek();

    void FAllocate();

    void StatFs();

    void StatX();
}