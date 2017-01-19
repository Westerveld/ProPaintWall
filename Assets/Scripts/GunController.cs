using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class GunController : NetworkBehaviour
{
    public GameObject paintballPrefab;
    private float lastTime, interval;
    private int ammo;

    void Start()
    {
        ammo = 30;
        lastTime = 0;
    }

    void Update()
    {
       
        if (Input.GetButtonDown("Fire1") && ammo > 0 && Time.time > lastTime + interval)
        {
            print("Fire");
            CmdFirePaintBall();
            ammo--;
            lastTime = Time.time;
        }
    }

   [Command]
    void CmdFirePaintBall()
    {
        print("CMDFIre");
        GameObject go =  (GameObject)Instantiate(paintballPrefab, transform.FindChild("Spawn Point").position, Quaternion.identity);
        NetworkServer.Spawn(go);
    }

    public void AmmoRefill()
    {
        ammo = 30;
    }
}
