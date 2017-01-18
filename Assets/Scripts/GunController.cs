using UnityEngine;
using System.Collections;

public class GunController : MonoBehaviour
{
    public GameObject paintballPrefab;
    public float shootForce;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject paintball = Instantiate(paintballPrefab);
            paintball.transform.position = transform.FindChild("Spawn Point").position;
            paintball.GetComponent<Rigidbody>().AddForce(-transform.right * shootForce);
            Destroy(paintball, 2f);
        }
    }
}
