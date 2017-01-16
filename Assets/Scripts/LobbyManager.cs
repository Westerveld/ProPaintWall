using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class LobbyManager : NetworkBehaviour {

    int currNumPlayers = 1, numOfTeams = 2;
    bool freeForAll = false;
    bool teamGame = true;

    NetworkLobbyManager NM;
    void Start()
    {
        NM = GetComponent<NetworkLobbyManager>();
    }
    
    public void StartServer()
    {
        NM.StartServer();
    }

    public void JoinServer()
    {
        NM.StartClient();
    }
}
