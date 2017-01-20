using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;
public class LobbyUISetup : NetworkBehaviour {
    public Text playerName, Results;
    Team teamToJoin;
    public NetworkPlayer localPlayer;
    GameManager gm;
    PaintableObjectManager pm;


    public delegate void ResetObject();

    [SyncEvent]
    public static event ResetObject EventResetObject;

    bool ready = false;

	// Use this for initialization
	void Start () {
        gm = GameObject.Find("RC").GetComponent<GameManager>();
        pm = GameObject.Find("PaintableObjectManager").GetComponent<PaintableObjectManager>();

    }

    public void JoinRedTeam()
    {
        teamToJoin = Team.RedTeam;
        ready = true;
    }

    public void JoinBlueTeam()
    {
        teamToJoin = Team.BlueTeam;
        ready = true;
    }

    public void AutoFill()
    {

    }

    public void Ready()
    {
        if(ready)
        { 
            localPlayer.CmdUpdateName(playerName.text);
            localPlayer.CmdUpdateTeam(teamToJoin);
            localPlayer.inGame = true;
            gm.Spawn(localPlayer.gameObject);
            ready = false;
            //gm.endTime = Time.time + 180f;
        }
    }

    public void ResetGame()
    {
        localPlayer.team = Team.NoTeam;
        gm.Spawn(localPlayer.gameObject);
        localPlayer.inGame = false;
        EventResetObject.Invoke();
        Results.text = pm.GetResults();

    }

}
