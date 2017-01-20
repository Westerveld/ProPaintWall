using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class LobbyUISetup : MonoBehaviour {
    public Text playerName, test;
    Team teamToJoin;
    public NetworkPlayer localPlayer;
    public GameManager gm;


	// Use this for initialization
	void Start () {
	}

    public void JoinRedTeam()
    {
        teamToJoin = Team.RedTeam;
    }

    public void JoinBlueTeam()
    {
        teamToJoin = Team.BlueTeam;
    }

    public void AutoFill()
    {

    }

    public void Ready()
    {
        localPlayer.CmdUpdateName(playerName.text);
        localPlayer.CmdUpdateTeam(teamToJoin);
        gm.Spawn(localPlayer.gameObject);
        gm.endTime = Time.time + 180f;
    }


	// Update is called once per frame
	void Update () {
	
	}
}
