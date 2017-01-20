using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Spawn(GameObject go)
    {
        if(go.GetComponent<NetworkPlayer>().team == Team.BlueTeam)
        {
            Vector3 spawnPos = GameObject.FindGameObjectWithTag("BlueSpawn").transform.position;
            float randX = Random.Range(-2.5f, 2.5f);
            float randZ = Random.Range(-2.5f, 2.5f);
            spawnPos.x += randX;
            spawnPos.z += randZ;
            go.transform.position = spawnPos;

        }
        if (go.GetComponent<NetworkPlayer>().team == Team.RedTeam)
        {
            Vector3 spawnPos = GameObject.FindGameObjectWithTag("RedSpawn").transform.position;
            float randX = Random.Range(-2.5f, 2.5f);
            float randZ = Random.Range(-2.5f, 2.5f);
            spawnPos.x += randX;
            spawnPos.z += randZ;
            go.transform.position = spawnPos;
        }
    }
}
