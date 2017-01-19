using UnityEngine;

public class JumpPadController : MonoBehaviour
{
    public Vector3 jumpVelocity;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Rigidbody>().velocity = jumpVelocity;
        }
    }
}
