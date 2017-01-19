using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.Networking;
using System.Collections;

public class NetworkPlayer : NetworkBehaviour
{
    public Camera cam;
    public AudioListener audioListener;
    FPSController fpsController; 

    // Use this for initialization
    void Start () {
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
	
}
