using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.Networking;
using System.Collections;

public class NetworkPlayer : NetworkBehaviour {

    public Camera cam;
    // Use this for initialization
    void Start () {
	    if(isLocalPlayer)
        {
            GetComponent<FirstPersonController>().enabled = true;
            cam.enabled = true;
            GetComponentInChildren<AudioListener>().enabled = true;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
