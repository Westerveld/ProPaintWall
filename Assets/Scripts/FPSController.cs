using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class FPSController : MonoBehaviour
{
    public float moveSpeed;
    public float sprintModifier;
    public float jumpSpeed;
    public Camera firstPersonCamera;
    public float maximumSlopeAngle;
    public Vector2 lookSensitivity;

    private Rigidbody rigidBody;
    private bool grounded = false;
    private Vector3 collisionNormal = Vector3.up;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 moveVelocity = transform.forward * CrossPlatformInputManager.GetAxis("Vertical") + 
                               transform.right * CrossPlatformInputManager.GetAxis("Horizontal");
        moveVelocity = Vector3.ProjectOnPlane(moveVelocity, collisionNormal);
        moveVelocity *= moveSpeed * (CrossPlatformInputManager.GetButton("Sprint") ? sprintModifier : 1f);

        Vector3 newVelocity = rigidBody.velocity;
        newVelocity.x = moveVelocity.x;
        newVelocity.z = moveVelocity.z;

        if (CrossPlatformInputManager.GetButtonDown("Jump") && grounded)
        {
            newVelocity.y = jumpSpeed;
        }

        rigidBody.velocity = newVelocity;

        firstPersonCamera.transform.Rotate(Vector3.left * CrossPlatformInputManager.GetAxis("LookVertical") * lookSensitivity.y * Time.deltaTime);
        transform.Rotate(Vector3.up * CrossPlatformInputManager.GetAxis("LookHorizontal") * lookSensitivity.x * Time.deltaTime);

        grounded = false;
    }

    void OnCollisionStay(Collision collision)
    {
        foreach (ContactPoint point in collision.contacts)
        {
            if (point.thisCollider == GetComponent<SphereCollider>() && point.normal.y > (Quaternion.Euler(maximumSlopeAngle, 0f, 0f) * Vector3.forward).y)
            {
                grounded = true;
                collisionNormal = point.normal;
            }
        }
    }

    void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
