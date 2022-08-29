using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;

public class NetworkPlayer : NetworkBehaviour
{
    public Vector2 Player1Position = new Vector2(-0.6f, 0.5f);
    public Vector2 Player2Position = new Vector2(0.6f, 0.5f);
    public Material MaterialP1 = null;
    public Material MaterialP2 = null;
    public float HandHueShift = 0.2f;
    public override void OnNetworkSpawn()
    {
        DisableClientInput();
        SetMaterials();
    }

    private void DisableClientInput()
    {
        
        if (!IsOwner)
        {
            var clientXROrigin = GetComponent<XROrigin>();
            var clientLocomotion = GetComponent<LocomotionSystem>();
            var clientMoveProvider = GetComponent<ActionBasedContinuousMoveProvider>();
            var clientTurnProvider = GetComponent<ActionBasedContinuousTurnProvider>();
            var clientControllers = GetComponentsInChildren<ActionBasedController>();
            var clientRays = GetComponentsInChildren<RayManager>();
            var clientHaptics = GetComponentsInChildren<ControllerHapticManager>();
            var clientHead = GetComponentInChildren<TrackedPoseDriver>();
            var clientCamera = GetComponentInChildren<Camera>();
            var clientAudioListener = GetComponentInChildren<AudioListener>();
            var clientLIV = GetComponent<LIV.SDK.Unity.LIV>();
            var clientWindowEncoder = GetComponent<WindowEncoder>();
            //var clientOVRManager = gameObject.transform.Find("OVRManager").gameObject;

            clientXROrigin.enabled = false;
            clientLocomotion.enabled = false;
            clientCamera.enabled = false; 
            clientAudioListener.enabled = false;
            clientMoveProvider.enabled = false;
            clientTurnProvider.enabled = false;
            clientHead.enabled = false;
            clientLIV.enabled = false;
            clientWindowEncoder.enabled = false;
            foreach (var ray in clientRays)
            {
                ray.RaySwitch = false;
            }
            foreach (var controller in clientControllers)
            {
                controller.enabled = false;
            }
            foreach (var haptic in clientHaptics)
            {
                Destroy(haptic);
            }
            clientLIV.enabled = false;
            //clientOVRManager.SetActive(false);
        }
    }
    private void SetMaterials()
    {
        var HeadRenderer = transform.Find("Camera Offset").Find("Main Camera").Find("HeadCube").gameObject.GetComponent<Renderer>();
        var LHandMat = transform.Find("Camera Offset").Find("LeftHand Controller").Find("LHand").GetComponent<Renderer>().material;
        var RHandMat = transform.Find("Camera Offset").Find("RightHand Controller").Find("RHand").GetComponent<Renderer>().material;
        if (IsOwnedByServer)
        {
            HeadRenderer.material = MaterialP1;
        }
        else 
        {
            HeadRenderer.material = MaterialP2;
            float LH, LS, LV; float RH, RS, RV;
            Color.RGBToHSV(LHandMat.color, out LH, out LS, out LV);
            Color.RGBToHSV(RHandMat.color, out RH, out RS, out RV);
            var LColor = Color.HSVToRGB(LH + HandHueShift, RS, RV);
            var RColor = Color.HSVToRGB(RH + HandHueShift, RS, RV);
            LColor.a = LHandMat.color.a; RColor.a = RHandMat.color.a;
            LHandMat.color = LColor;
            RHandMat.color = RColor;
        }
    }
    private void Start()
    {
        if (IsHost)
        {
            transform.position = new Vector3(Player1Position.x, transform.position.y, transform.position.z + Player1Position.y); 
        }
        else
        {
            transform.position = new Vector3(Player2Position.x, transform.position.y, transform.position.z + Player2Position.y);
        }
    }
}
