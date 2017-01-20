using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public float endTime = 180f;
    public bool gameOver = false;
    public Camera respawnCamera;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time < endTime)
        {
            gameOver = true;
        }
    }

    public void Spawn(GameObject go)
    {
        if (go.GetComponent<NetworkPlayer>().team == Team.BlueTeam)
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

    public IEnumerator Respawn(GameObject go)
    {
        go.transform.position = new Vector3();
        go.GetComponent<FPSController>().isDead = true;
        go.GetComponentInChildren<Camera>().enabled = false;
        respawnCamera.enabled = true;
        yield return new WaitForSeconds(3f);
        Spawn(go);
        go.GetComponent<PlayerHealth>().currentHealth = 100;
        go.GetComponentInChildren<Camera>().enabled = true;
        go.GetComponent<FPSController>().isDead = false;
    }
}
