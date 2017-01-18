using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class Paintball : NetworkBehaviour
{
    int moveSpeed = 1;
	// Use this for initialization
	void Start () {

        gameObject.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * moveSpeed);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
