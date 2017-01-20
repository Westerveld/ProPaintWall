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
    public Team team;

    [SyncVar]
    string playerName;

    

    // Use this for initialization
    void Start()
    {
        tm = gameObject.GetComponentInChildren<TextMesh>();
        cam = GameObject.Find("Menu Camera").GetComponent<Camera>();
        if(cam != null)
        {
            if (isLocalPlayer)
            {
                cam.GetComponent<LobbyUISetup>().localPlayer = this;
            }
        }
    }

    void Update()
    {
        if(cam == null)
        {
            cam = GameObject.Find("Menu Camera").GetComponent<Camera>();
            if (isLocalPlayer)
            {
                cam.GetComponent<LobbyUISetup>().localPlayer = this;
            }
        }

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
        // tm.text = playerName;
        tm.text = team.ToString();

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
