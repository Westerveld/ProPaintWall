using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerHealth : NetworkBehaviour {

    public const int maxHealth = 100;

    [SyncVar]
    public int currentHealth = maxHealth;

    private GameManager gm;

    void Start()
    {
        gm = GameObject.Find("RC").GetComponent<GameManager>();
    }
    public void RemoveHealth(int amount)
    {
        if (!isLocalPlayer)
            return;
        currentHealth -= amount;
        if(currentHealth<= 0)
        {
            GetComponent<FPSController>().isDead = true;
            StartCoroutine(gm.Respawn(gameObject));
        }
    }
}
