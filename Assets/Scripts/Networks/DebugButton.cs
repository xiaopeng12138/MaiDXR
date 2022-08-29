using UnityEngine;
using Unity.Netcode;

public class DebugButton : MonoBehaviour
{
    private void OnGUI() {
        GUILayout.BeginArea(new Rect(10, 10, 100, 300));
        if (JsonConfig.HasKey("MultiplayerDebugButton")) 
        {
            if (!JsonConfig.GetBoolean("MultiplayerDebugButton")) return;
                if (GUILayout.Button("Host")) GetComponent<StartManager>().StartHost();
                if (GUILayout.Button("Client")) GetComponent<StartManager>().StartClient();
                if (GUILayout.Button("Stop")) GetComponent<StartManager>().StopAll();
        }
        else
            JsonConfig.SetBoolean("MultiplayerDebugButton", false);
        GUILayout.EndArea();
    }
}
