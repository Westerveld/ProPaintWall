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

    public void Spawn(GameObject player)
    {
        if (player.GetComponent<NetworkPlayer>().team == Team.BlueTeam)
        {
            Vector3 spawnPos = GameObject.Find("Blue Team").transform.position;
            float randX = Random.Range(-2.5f, 2.5f);
            float randZ = Random.Range(-2.5f, 2.5f);
            spawnPos.x += randX;
            spawnPos.z += randZ;
            player.transform.position = spawnPos;
            player.GetComponent<PlayerHealth>().currentHealth = 100;
            player.GetComponent<GunController>().AmmoRefill();

        }
        else if (player.GetComponent<NetworkPlayer>().team == Team.RedTeam)
        {
            Vector3 spawnPos = GameObject.Find("Red Team").transform.position;
            float randX = Random.Range(-2.5f, 2.5f);
            float randZ = Random.Range(-2.5f, 2.5f);
            spawnPos.x += randX;
            spawnPos.z += randZ;
            player.transform.position = spawnPos;
            player.GetComponent<PlayerHealth>().currentHealth = 100;
            player.GetComponent<GunController>().AmmoRefill();
        }
        else if(player.GetComponent<NetworkPlayer>().team == Team.NoTeam)
        {
            Vector3 spawnPos = GameObject.Find("No Team").transform.position;
            float randX = Random.Range(-2.5f, 2.5f);
            float randZ = Random.Range(-2.5f, 2.5f);
            spawnPos.x += randX;
            spawnPos.z += randZ;
            player.transform.position = spawnPos;
            player.GetComponent<PlayerHealth>().currentHealth = 100;
            player.GetComponent<GunController>().AmmoRefill();
        }
    }

    public IEnumerator Respawn(GameObject player)
    {
        PlayerManager playerManager = player.GetComponent<PlayerManager>();
        playerManager.respawnTime = 3;

        player.transform.position = new Vector3(55f, 1f, 55f);
        player.GetComponent<FPSController>().isDead = true;
        player.GetComponentInChildren<Camera>().enabled = false;
        respawnCamera.enabled = true;

        while (playerManager.respawnTime > 0)
        {
            yield return new WaitForSeconds(1f);
            playerManager.respawnTime--;
        }
        
        Spawn(player);
        player.GetComponentInChildren<Camera>().enabled = true;
        player.GetComponent<FPSController>().isDead = false;
    }
}
