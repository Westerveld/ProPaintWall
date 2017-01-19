using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class GunController : NetworkBehaviour
{
    public GameObject paintballPrefab;
    public float bulletForce;
    public int ammo;

    private float lastTime, interval = 0.5f;

    PauseMenu pm;

    void Awake()
    {
        ammo = 30;
        lastTime = 0;
        pm = GameObject.FindGameObjectWithTag("GM").GetComponent<PauseMenu>();
    }

    void Update()
    {
       
        if(!isLocalPlayer)
        {
            return;
        }

        if (Input.GetButtonDown("Fire1") && ammo > 0 && Time.time > lastTime + interval && pm.paused != false)
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
        go.GetComponent<Rigidbody>().AddForce(Vector3.forward * bulletForce);
        NetworkServer.Spawn(go);
    }

    public void AmmoRefill()
    {
        ammo = 30;
    }
}
