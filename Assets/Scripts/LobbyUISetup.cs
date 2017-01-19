using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class LobbyUISetup : MonoBehaviour {
    public Text playerName, test;
    Team teamToJoin;
    public NetworkPlayer localPlayer;
	// Use this for initialization
	void Start () {
	
	}

    public void JoinRedTeam()
    {
        teamToJoin = Team.RedTeam;
    }

    public void JoinGreenTeam()
    {
        teamToJoin = Team.GreenTeam;
    }
    public void JoinBlueTeam()
    {
        teamToJoin = Team.BlueTeam;
    }
    public void JoinYellowTeam()
    {
        teamToJoin = Team.YellowTeam;
    }

    public void Ready()
    {
        localPlayer.CmdUpdateName(playerName.text);
        localPlayer.CmdUpdateTeam(teamToJoin);
    }


	// Update is called once per frame
	void Update () {
	
	}
}
