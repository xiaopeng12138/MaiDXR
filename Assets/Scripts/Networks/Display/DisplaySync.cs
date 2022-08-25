using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using uWindowCapture;
using uPacketDivision;
using uNvPipe;
using System.Runtime.InteropServices;

public class DisplaySync : NetworkBehaviour
{
    public WindowEncoder Encoder;
    public uNvPipeDecoder Decoder;
    public uNvPipeDecodedTexture DecoderTexture;
    public uint maxPacketSize = 4096;
    private UwcWindow DisplayP1Window;
    private Material DisplayP2Mat;
    Divider divider = new Divider();
    public uint timeout = 100;
    Assembler assembler = new Assembler();
    bool isInitialized = false;
    void Start()
    {
        NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnectedCallback;

        var DisplayP1 = GameObject.FindGameObjectWithTag("DisplayP1");
        var DisplayP2 = GameObject.FindGameObjectWithTag("DisplayP2");

        Encoder.texture = DisplayP1.GetComponent<UwcWindowTexture>();
        Decoder = DisplayP2.GetComponent<uNvPipeDecoder>();
        DecoderTexture = DisplayP2.GetComponent<uNvPipeDecodedTexture>();
        Decoder.enabled = false;
        DecoderTexture.enabled = false;
        DisplayP2Mat = DisplayP2.GetComponent<Renderer>().material;

        divider.maxPacketSize = maxPacketSize;

        if (IsOwner)
        {
            Debug.Log("Add Listener");
            Encoder.encoder.onEncoded.AddListener(OnEncoded);
        }
    }
    void Update()
    {

    }
    public void OnEncoded(System.IntPtr data, int size)
    {
        //Debug.Log("OnEncoded");
        int[] winSize = new int[2]{Encoder.setting.width, Encoder.setting.height};
        SendSizeServerRpc(winSize);

        
        divider.Divide(data, (uint)size);
        for (uint i = 0; i < divider.GetChunkCount(); ++i)
        {
            SendDataServerRpc(divider.GetChunk(i));
        }
    }
    private void OnClientDisconnectedCallback(ulong callBack)
    {
        Decoder.enabled = false;
        DecoderTexture.enabled = false;
        isInitialized = false;
    }

    //[ServerRpc(Delivery = RpcDelivery.Unreliable)]
    [ServerRpc]
    private void SendDataServerRpc(byte[] bytes)
    {
        SetDataClientRpc(bytes);
    }

    //[ClientRpc(Delivery = RpcDelivery.Unreliable)]
    [ClientRpc]
    private void SetDataClientRpc(byte[] bytes)
    {
        if (IsOwner) return;
        assembler.timeout = timeout;
        assembler.Add(bytes);
        CheckPacketEvent();
    }

    [ServerRpc]
    private void SendSizeServerRpc(int[] ints)
    {
        SetSizeClientRpc(ints);
    }

    //[ClientRpc(Delivery = RpcDelivery.Unreliable)]
    [ClientRpc]
    private void SetSizeClientRpc(int[] ints)
    {
        if (IsOwner) return;
        if (isInitialized) return;

        var width = ints[0];
        var height = ints[1];
        Debug.LogFormat("Start decoder: width => {0}, height => {1}", width, height);
        Decoder.width = width;
        Decoder.height = height;
        Decoder.enabled = true;
        DecoderTexture.enabled = true;
        DecoderTexture.gameObject.GetComponent<Renderer>().material.SetTextureScale("_MainTex",new Vector2(width/height < 0.6 ? 1f : 0.5f, -1));

        isInitialized = true;
    }

    void CheckPacketEvent()
    {
        if (!isInitialized) return;

        switch (assembler.GetEventType())
        {
            case uPacketDivision.EventType.FrameCompleted:
            {
                var data = assembler.GetAssembledData<byte>();
                int size = data.Length;
                var handle = GCHandle.Alloc(data, GCHandleType.Pinned);
                var ptr = handle.AddrOfPinnedObject();
                Decoder.Decode(ptr, (int)size);
                handle.Free();
                break;
            }
            case uPacketDivision.EventType.PacketLoss:
            {
                Debug.LogError("packet loss");
                break;
            }
            default:
            {
                break;
            }
        }
    }
}
