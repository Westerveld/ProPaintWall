using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

    float startTime, lifeTime = 5;

    void Awake()
    {
        startTime = Time.time;
    }

    void FixedUpdate()
    {
        if(Time.time > startTime + lifeTime)
        {
            Destroy(gameObject);
        }
    }
}
