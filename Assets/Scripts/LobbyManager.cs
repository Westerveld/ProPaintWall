using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class LobbyManager : NetworkBehaviour {

    public GameObject[] ServerButtons;
    public NetworkLobbyManager NM;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void HostServer()
    {
        foreach(GameObject i in ServerButtons)
        {
            i.SetActive(false);
        }
        NM.StartServer();
        if (NM.isNetworkActive)
        {
            print(true);
        }
    }

    public void JoinServer()
    {
        foreach (GameObject i in ServerButtons)
        {
            i.SetActive(false);
        }
        NM.StartClient();
        if(!NM.IsClientConnected())
        {
            print(false);
        } else
        {
            print(true);
        }
    }

    public void OnConnectedToServer()
    {
    }
}
