using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public GameObject player;
    public Text ammoText, healthText, respawnText, gameTimeText;

    void Awake()
    {
        if (!player)
        {
            player = transform.parent.gameObject;
        }

        if (player.GetComponent<NetworkPlayer>().isLocalPlayer)
        {
            GetComponent<Canvas>().enabled = false;
        }
        else
        {
            GetComponent<Canvas>().enabled = true;
        }
    }

    void Update()
    {
        GunController gunController = player.GetComponent<GunController>();
        if (gunController)
        {
            ammoText.text = "Ammo: " + gunController.ammo;
        }

        PlayerHealth health = player.GetComponent<PlayerHealth>();
        if(health)
        {
            healthText.text = "Health: " + health.currentHealth;
        }

        PlayerManager playerManager = player.GetComponent<PlayerManager>();
        if (playerManager)
        {
            if (playerManager.respawnTime == 0)
            {
                respawnText.enabled = false;
            }
            else
            {
                respawnText.enabled = true;
                respawnText.text = "Time until respawn: " + playerManager.respawnTime + "s";
            }
        }

        if (GameObject.FindGameObjectWithTag("GM"))
        {
            PauseMenu pauseMenu = GameObject.FindGameObjectWithTag("GM").GetComponent<PauseMenu>();
            if (pauseMenu)
            {
                gameTimeText.text = "Time remaining: " + pauseMenu.remainingTime + "s";
            }
        }
    }
}
