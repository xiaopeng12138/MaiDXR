using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;

public class StartUp : MonoBehaviour
{
    public List<Transform> PlayerIOs;
    public List<Transform> PlayerIOsOpposite;
    public Transform Player1Anchor;
    public Transform Player2Anchor;
    public Transform SelectButton;
    bool isHost = true;
    string hostIP = "127.0.0.1";
    int hostPort = 7777;
    void Start()
    {
        if (JsonConfig.HasKey("IsHost")) isHost = JsonConfig.GetBoolean("IsHost");
        else JsonConfig.SetBoolean("IsHost", isHost);

        if (JsonConfig.HasKey("HostIP")) hostIP = JsonConfig.GetString("HostIP");
        else JsonConfig.SetString("HostIP", hostIP);

        if (JsonConfig.HasKey("HostPort")) hostPort = JsonConfig.GetInt("HostPort");
        else JsonConfig.SetInt("HostPort", hostPort);

        if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer) 
        {
            if (isHost)
            {
                NetworkManager.Singleton.StartHost();
                foreach (var IO in PlayerIOs)
                    IO.position = new Vector3(Player1Anchor.position.x, IO.position.y, IO.position.z);
                foreach (var IO in PlayerIOsOpposite)
                    IO.position = new Vector3(Player2Anchor.position.x, IO.position.y, IO.position.z);
            }
            else
            {
                NetworkManager.Singleton.StartClient();
                foreach (var IO in PlayerIOs)
                    IO.position = new Vector3(Player2Anchor.position.x, IO.position.y, IO.position.z);
                foreach (var IO in PlayerIOsOpposite)
                    IO.position = new Vector3(Player1Anchor.position.x, IO.position.y, IO.position.z);
                SelectButton.localScale = new Vector3(SelectButton.localScale.x * -1, SelectButton.localScale.y, SelectButton.localScale.z);
            }
            GetComponent<UnityTransport>().SetConnectionData(hostIP, (ushort)hostPort);
        }
    }
}
