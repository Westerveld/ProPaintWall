using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public int respawnTime = 0;

    PauseMenu pm;
	// Use this for initialization
	void Start () {
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
            GetComponent<FPSController>().enabled = false;
        }
        else
        {
            GetComponent<FPSController>().enabled = true;
        }
	}
}
