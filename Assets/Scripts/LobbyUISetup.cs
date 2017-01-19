using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class LobbyUISetup : MonoBehaviour {
    public Text playerName, test;

    public NetworkPlayer localPlayer;
	// Use this for initialization
	void Start () {
	
	}

    public void Ready()
    {
        localPlayer.CmdUpdateName(playerName.text);
    }


	// Update is called once per frame
	void Update () {
	
	}
}
