using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.Networking;
using System.Collections;
public class NetworkPlayer : NetworkBehaviour
{
    Camera cam;
    AudioListener audioListener;
    FPSController fpsController;
    bool teamSelect = true;
    TextMesh tm;
    [SyncVar]
    Team team;
    [SyncVar]
    string playerName;
    // Use this for initialization
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("Lobby").GetComponent<Camera>();
     tm = gameObject.GetComponentInChildren<TextMesh>();
        if (isLocalPlayer)
        {
            cam.GetComponent<LobbyUISetup>().localPlayer = this;
        }
    }

    void Update()
    {
        if (teamSelect)
        {
            GetComponent<FPSController>().enabled = false;
            cam.enabled = true;
            GetComponent<FPSController>().firstPersonCamera.enabled = false;
        }
        else
        {
            GetComponent<FPSController>().enabled = true;
            cam.enabled = false;
            GetComponent<FPSController>().firstPersonCamera.enabled = true;
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            teamSelect = !teamSelect;
        }


        tm.text = playerName;

    }


    [Command]
   public void CmdUpdateTeam(Team t)
    {
        this.team = t;
        
    }

    [Command]
    public void CmdUpdateName(string n)
    {
        this.playerName = n;
       
    }

}
