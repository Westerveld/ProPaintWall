using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerManager : MonoBehaviour {

    PauseMenu pm;
	// Use this for initialization
	void Awake () {
        pm = GameObject.FindGameObjectWithTag("GM").GetComponent<PauseMenu>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Pause"))
        {
            pm.LoadMenu();
        }
        if(pm.paused)
        {
            GetComponent<FirstPersonController>().enabled = false;
        }
        else
        {
            GetComponent<FirstPersonController>().enabled = true;
        }
	}
}
