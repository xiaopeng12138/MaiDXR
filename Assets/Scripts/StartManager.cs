using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Netcode.Transports.Enet;
using UnityEngine.UI;


public class StartManager : MonoBehaviour
{
    public List<Transform> PlayerIOs;
    public List<Transform> PlayerIOsOpposite;
    public Transform Player1Anchor;
    public Transform Player2Anchor;
    public Transform SelectButton;
    public GameObject XRLocal;
    public Button StartHostButton;
    public Button StartClientButton;
    private PlayerSettingManager PlayerSettingManager;
    string hostIP = "127.0.0.1";
    int hostPort = 7777;
    void Start()
    {
        if (JsonConfig.HasKey("HostIP")) hostIP = JsonConfig.GetString("HostIP");
        else JsonConfig.SetString("HostIP", hostIP);

        if (JsonConfig.HasKey("HostPort")) hostPort = JsonConfig.GetInt("HostPort");
        else JsonConfig.SetInt("HostPort", hostPort);

        GetComponent<EnetTransport>().Address = hostIP;
        GetComponent<EnetTransport>().Port = (ushort)hostPort;

        PlayerSettingManager = XRLocal.GetComponent<PlayerSettingManager>();
    }
    public void StartHost()
    {
        if (NetworkManager.Singleton.IsHost) 
            return;
        if (NetworkManager.Singleton.IsClient) 
            NetworkManager.Singleton.Shutdown();

        NetworkManager.Singleton.StartHost();

        foreach (var IO in PlayerIOs)
            IO.position = new Vector3(Player1Anchor.position.x, IO.position.y, IO.position.z);
        foreach (var IO in PlayerIOsOpposite)
            IO.position = new Vector3(Player2Anchor.position.x, IO.position.y, IO.position.z);

        PlayerSettingManager.SetTarget(NetworkManager.Singleton.LocalClient.PlayerObject.gameObject);
        XRLocal.SetActive(false);
        StartHostButton.interactable = false;
    }
    public void StartClient()
    {
        if (NetworkManager.Singleton.IsClient) 
            return;
        if (NetworkManager.Singleton.IsHost)
            NetworkManager.Singleton.Shutdown();

        NetworkManager.Singleton.StartClient();

        foreach (var IO in PlayerIOs)
            IO.position = new Vector3(Player2Anchor.position.x, IO.position.y, IO.position.z);
        foreach (var IO in PlayerIOsOpposite)
            IO.position = new Vector3(Player1Anchor.position.x, IO.position.y, IO.position.z);
        SelectButton.localScale = new Vector3(SelectButton.localScale.x * -1, SelectButton.localScale.y, SelectButton.localScale.z);

        PlayerSettingManager.SetTarget(NetworkManager.Singleton.LocalClient.PlayerObject.gameObject);
        XRLocal.SetActive(false);
        StartClientButton.interactable = false;
    }
    public void StopAll()
    {
        NetworkManager.Singleton.Shutdown();
        PlayerSettingManager.SetTarget(XRLocal);
        XRLocal.SetActive(true);
        StartHostButton.interactable = true;
        StartClientButton.interactable = true;
    }
}
