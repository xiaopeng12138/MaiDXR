using UnityEngine;
using System.Collections;
using uWindowCapture;
using Unity.Netcode;

public class WindowEncoder : NetworkBehaviour
{
    [SerializeField]
    public UwcWindowTexture texture = null;

    [SerializeField]
    public uNvEncoder.Encoder encoder = null;

    [SerializeField]
    public uNvEncoder.EncoderDesc setting = new uNvEncoder.EncoderDesc
    {
        width = 1920,
        height = 1080,
        frameRate = 24,
        format = uNvEncoder.Format.B8G8R8A8_UNORM,
        bitRate = 98304,
        maxFrameSize = 4096,
    };

    public int idrFrameIntervalFrame = 24;
    int idrFrameCounter_ = 0;
    public int ResolutionDivider = 2;
    public Texture2D sTexture;
    

    void Start()
    {
        if (!IsOwner) return;
        StartCoroutine(EncodeLoop());
    }

    void OnDisable()
    {
        if (!IsOwner) return;
        StopAllCoroutines();
        encoder.Destroy();
    }

    RenderTexture rt;
    void Resize()
    {
        Graphics.Blit(texture.window.texture, rt);
        sTexture.ReadPixels(new Rect(0,0,setting.width, setting.height),0,0);
        sTexture.Apply();
    }
    IEnumerator EncodeLoop()
    {
        for (;;)
        {
            if (texture.window != null) break;
            yield return new WaitForEndOfFrame();
        }

        setting.width = texture.window.width / ResolutionDivider;
        setting.height = texture.window.height / ResolutionDivider;
        encoder.Create(setting);

        rt = new RenderTexture(setting.width, setting.height, 24);
        sTexture = new Texture2D(setting.width, setting.height, TextureFormat.BGRA32, false);
        RenderTexture.active = rt;

        for (;;)
        {
            if (setting.frameRate < 60)
            {
                yield return new WaitForSeconds(1f / setting.frameRate);
            }

            bool idr = idrFrameCounter_++ % idrFrameIntervalFrame == 0;
            Resize();
            encoder.Encode(sTexture, idr);
            encoder.Update();
        }
    }

    [ContextMenu("Reconfigure")]
    public void Reconfigure()
    {
        if (encoder == null) return;
        encoder.Reconfigure(setting);
    }
    
    public ComputeShader ResizeShader;
    Texture2D Resize(ComputeShader shader, Texture2D inputTexture, int divideSize) 
    {
        Texture2D t = new Texture2D(inputTexture.width/divideSize, inputTexture.height/divideSize, TextureFormat.BGRA32, false);
        int k = shader.FindKernel("Resize");
        shader.SetInt("divideSize", divideSize);        
        shader.SetTexture(k, "inputTexture", inputTexture);
        shader.SetTexture(k, "outputTexture", t);
        shader.Dispatch(k, inputTexture.width / 8, inputTexture.height / 8, 1);
        return t;
    }
}