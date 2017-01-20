using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerHealth : NetworkBehaviour {

    public const int maxHealth = 100;

    [SyncVar]
    public int currentHealth = maxHealth;

    public void RemoveHealth(int amount)
    {
        if (!isServer)
            return;
        currentHealth -= amount;
        if(currentHealth<= 0)
        {
            GetComponent<FPSController>().isDead = true;
        }
    }
}
