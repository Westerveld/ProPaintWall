using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.Networking;
using System.Collections;
public class NetworkPlayer : NetworkBehaviour
{
    Camera cam;
    AudioListener audioListener;
    FPSController fpsController;
    
    bool inGame = false;

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
        if (inGame)
        {
            GetComponent<FPSController>().enabled = true;

            if (isLocalPlayer)
            {
                cam.enabled = false;
                GetComponent<FPSController>().firstPersonCamera.enabled = true;
                GetComponent<GunController>().enabled = true;
                GetComponent<PlayerManager>().enabled = true;
                Cursor.lockState = CursorLockMode.Locked;
            }


        }
        else
        {

            GetComponent<FPSController>().enabled = false;

            if (isLocalPlayer)
            {
                cam.enabled = true;
                GetComponent<FPSController>().firstPersonCamera.enabled = false;
                GetComponent<GunController>().enabled = false;
                GetComponent<PlayerManager>().enabled = false;
                Cursor.lockState = CursorLockMode.None;
            }
        }

        Begin();
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
   
    void Begin()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            inGame = !inGame;
        }
    }
}
