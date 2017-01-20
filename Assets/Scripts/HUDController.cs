﻿using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public GameObject player;
    public Text ammoText, healthText;

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
    }
}
