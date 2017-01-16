using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.Networking;
using System.Collections;

public class NetworkPlayer : NetworkBehaviour {

    // Use this for initialization
    void Start () {
	    if(isLocalPlayer)
        {
            GetComponent<FirstPersonController>().enabled = true;
            GetComponentInChildren<Camera>().enabled = true;
            GetComponentInChildren<AudioListener>().enabled = true;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
