using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class NetworkPlayer : NetworkBehaviour
{
    public Vector2 Player1Position = new Vector2(-0.75f, 0);
    public Vector2 Player2Position = new Vector2(0.75f, 0);
    public override void OnNetworkSpawn()
    {
        //base.OnNetworkSpawn();
        DisableClientInput();
    }

    private void DisableClientInput()
    {
        if (!IsOwner)
        {
            var clientMoveProvider = GetComponent<ActionBasedContinuousMoveProvider>();
            var clientTurnProvider = GetComponent<ActionBasedContinuousTurnProvider>();
            var clientControllers = GetComponentsInChildren<ActionBasedController>();
            var clientRays = GetComponentsInChildren<RayManager>();
            var clientHaptics = GetComponentsInChildren<ControllerHapticManager>();
            var clientHead = GetComponentInChildren<TrackedPoseDriver>();
            var clientCamera = GetComponentInChildren<Camera>();
            var clientAudioListener = GetComponentInChildren<AudioListener>();
            var clientLIV = GetComponent<LIV.SDK.Unity.LIV>();
            //var clientOVRManager = gameObject.transform.Find("OVRManager").gameObject;

            clientCamera.enabled = false; 
            clientAudioListener.enabled = false;
            clientMoveProvider.enabled = false;
            clientTurnProvider.enabled = false;
            clientHead.enabled = false;
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
    private void Start()
    {
        if (IsHost)
        {
            transform.position = new Vector3(Player1Position.x, transform.position.y, Player1Position.y);
        }
        else
        {
            transform.position = new Vector3(Player2Position.x, transform.position.y, Player2Position.y);
        }
    }
}
