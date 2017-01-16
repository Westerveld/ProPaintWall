using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.Networking;
using System.Collections;

public class NetworkPlayer : NetworkBehaviour {

    public Camera cam;
    public AudioListener audioListener;
    // Use this for initialization
    void Start () {
	    if(isLocalPlayer)
        {
            GetComponent<FirstPersonController>().enabled = true;
            cam.enabled = true;
            audioListener.enabled = true;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
