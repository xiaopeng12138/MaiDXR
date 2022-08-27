using UnityEngine;
using System.IO;
using System.Runtime.InteropServices;
using Unity.Netcode;
namespace uNvEncoder.Examples
{

public class OutputEncodedDataToFile : NetworkBehaviour
{
    [SerializeField]
    string filePath = "test.h264";

    FileStream fileStream_;
    BinaryWriter binaryWriter_;

    public override void OnNetworkSpawn()
    {
        if (!IsOwner) return;
        fileStream_ = new FileStream(filePath, FileMode.Create, FileAccess.Write);
        binaryWriter_ = new BinaryWriter(fileStream_);
    }

    void OnApplicationQuit()
    {
        if (fileStream_ != null) 
        {
            fileStream_.Close();
        }

        if (binaryWriter_ != null) 
        {
            binaryWriter_.Close();
        }
    }

    public void OnData(System.IntPtr ptr, int size)
    {
        if (!enabled) return;

        if (ptr == System.IntPtr.Zero) return;

        var bytes = new byte[size];
        Marshal.Copy(ptr, bytes, 0, size);
        if (bytes == null) 
            return;    
        binaryWriter_.Write(bytes);
    }
}

}
