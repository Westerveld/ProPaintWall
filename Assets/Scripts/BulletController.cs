using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

    float lifeTime = 5;
    public Team team;
    public int damage = 20;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }


    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Paintable")
        {
            col.gameObject.GetComponent<PaintableObject>().HitObject(team, damage);
            Destroy(gameObject);
        }
        NetworkPlayer NP = col.gameObject.GetComponent<NetworkPlayer>();
        if (NP)
        {
            if (NP.team != team)
            {
                PlayerHealth enemy = col.gameObject.GetComponent<PlayerHealth>();
                enemy.RemoveHealth(damage);
                Destroy(gameObject);
            }
        }
    }
        
}
