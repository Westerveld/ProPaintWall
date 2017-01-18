using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class GunController : NetworkBehaviour
{
    public GameObject paintballPrefab;

    void Update()
    {
       
            if (Input.GetButtonDown("Fire1"))
            {
                print("Fire");
                CmdFirePaintBall();
            }
        

    }

   [Command]
    void CmdFirePaintBall()
    {
        print("CMDFIre");
        GameObject go =  (GameObject)Instantiate(paintballPrefab, transform.FindChild("Spawn Point").position, Quaternion.identity);
        NetworkServer.Spawn(go);
    }
}
