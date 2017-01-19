using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.Networking;
using System.Collections;

public class NetworkPlayer : NetworkBehaviour
{
    public Camera cam;
    public AudioListener audioListener;
    FPSController fpsController;
    bool teamSelect = true;

    // Use this for initialization
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("Lobby").GetComponent<Camera>();
        /*
	    if(isLocalPlayer)
        {
            GetComponent<FPSController>().enabled = true;
            cam.enabled = true;
            audioListener.enabled = true;
            //GetComponent<PlayerManager>().enabled = true;
        }
        */
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
    }

}
