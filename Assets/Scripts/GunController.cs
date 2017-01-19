using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class GunController : NetworkBehaviour
{
    public GameObject paintballPrefab;
    public float bulletForce;
    public int ammo;

    private float lastTime, interval = 0.1f;

    PauseMenu pm;
    Team team;
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

        if (Input.GetButton("Fire1") && ammo > 0 && Time.time > lastTime + interval && !pm.paused)
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
        go.GetComponent<Rigidbody>().AddForce(transform.FindChild("FirstPersonCharacter").forward * bulletForce);
        go.GetComponent<BulletController>().team = gameObject.GetComponent<NetworkPlayer>().team;
        NetworkServer.Spawn(go);
    }

    public void AmmoRefill()
    {
        ammo = 30;
    }
}
