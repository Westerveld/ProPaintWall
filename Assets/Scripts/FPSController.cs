using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Networking;

public class FPSController : NetworkBehaviour
{
    public float moveSpeed;
    public float sprintModifier;
    public float jumpSpeed;
    public Camera firstPersonCamera;
    public AudioListener audioListener;
    public float maximumSlopeAngle;
    public Vector2 lookSensitivity;
    public Vector2 gamepadLookSensitivity;
    public float crouchModifier;
    [SyncVar]
    public bool crouching = false;
    [SyncVar]
    public float speed;

    private delegate void JumpEvent();
    [SyncEvent]
    private event JumpEvent EventOnJump;

    private delegate void AmmoRefillEvent();
    [SyncEvent]
    private event AmmoRefillEvent EventOnAmmoRefill;

    private Rigidbody rigidBody;
    private bool grounded = false;
    private Vector3 collisionNormal = Vector3.up;

    private float capsuleHeight;
    private Vector3 characterScale;
    private float cameraPosition;
    private GunController gun;

    int health;
    int respawnTime;
    private bool localCrouching = false;
    
    void Start()
    {
        if (isLocalPlayer)
        {
            firstPersonCamera.enabled = true;
            audioListener.enabled = true;
            GetComponent<PlayerManager>().enabled = true;
        }

        rigidBody = GetComponent<Rigidbody>();
        capsuleHeight = GetComponent<CapsuleCollider>().height;
        cameraPosition = firstPersonCamera.transform.localPosition.y + capsuleHeight / 2f;
        gun = GetComponent<GunController>();

        EventOnJump += OnJump;
        EventOnAmmoRefill += OnAmmoRefill;
    }

    void Destroy()
    {
        EventOnJump -= OnJump;
        EventOnAmmoRefill -= OnAmmoRefill;
    }

    void FixedUpdate()
    {

        if (isLocalPlayer)
        {
            GroundCheck();

            Vector3 moveVelocity = transform.forward * CrossPlatformInputManager.GetAxis("Vertical") +
                               transform.right * CrossPlatformInputManager.GetAxis("Horizontal");
            moveVelocity = Vector3.ProjectOnPlane(moveVelocity, collisionNormal);
            moveVelocity *= moveSpeed * (CrossPlatformInputManager.GetButton("Sprint") ? sprintModifier : 1f);

            CmdSetSpeed(moveVelocity.magnitude);

            Vector3 newVelocity = rigidBody.velocity;
            newVelocity.x = moveVelocity.x;
            newVelocity.z = moveVelocity.z;

            if (CrossPlatformInputManager.GetButtonDown("Jump") && grounded)
            {
                newVelocity.y = jumpSpeed;
                CmdJump();
            }

            rigidBody.velocity = newVelocity;

            firstPersonCamera.transform.Rotate(Vector3.left * CrossPlatformInputManager.GetAxis("LookVertical") * lookSensitivity.y * Time.deltaTime);
            firstPersonCamera.transform.Rotate(Vector3.right * CrossPlatformInputManager.GetAxis("GamePadLookVertical") * gamepadLookSensitivity.y * lookSensitivity.y * Time.deltaTime);
            transform.Rotate(Vector3.up * CrossPlatformInputManager.GetAxis("LookHorizontal") * lookSensitivity.x * Time.deltaTime);
            transform.Rotate(Vector3.up * CrossPlatformInputManager.GetAxis("GamePadLookHorizontal") * gamepadLookSensitivity.x * lookSensitivity.x * Time.deltaTime);

            if (CrossPlatformInputManager.GetButton("Crouch") && !CrossPlatformInputManager.GetButton("Sprint"))
            {
                localCrouching = true;
            }
            else
            {
                localCrouching = false;
            }
            CmdSetCrouch(localCrouching);
        }

        transform.FindChild("Model").GetComponent<Animator>().SetFloat("Speed", speed);

        if ((isLocalPlayer && localCrouching) || crouching)
        {
            GetComponent<CapsuleCollider>().height = capsuleHeight / crouchModifier;
            GetComponent<CapsuleCollider>().center = new Vector3(GetComponent<CapsuleCollider>().center.x,
                                                                 ((capsuleHeight / crouchModifier) - capsuleHeight) / 2f,
                                                                 GetComponent<CapsuleCollider>().center.z);
            firstPersonCamera.transform.localPosition = new Vector3(firstPersonCamera.transform.localPosition.x,
                                                                    (cameraPosition / crouchModifier) - (capsuleHeight / 2f),
                                                                    firstPersonCamera.transform.localPosition.z);
        }
        else
        {
            GetComponent<CapsuleCollider>().height = capsuleHeight;
            GetComponent<CapsuleCollider>().center = new Vector3(GetComponent<CapsuleCollider>().center.x,
                                                                 0f,
                                                                 GetComponent<CapsuleCollider>().center.z);
            firstPersonCamera.transform.localPosition = new Vector3(firstPersonCamera.transform.localPosition.x,
                                                                    cameraPosition - (capsuleHeight / 2f),
                                                                    firstPersonCamera.transform.localPosition.z);
        }
        transform.FindChild("Model").GetComponent<Animator>().SetBool("Crouching", (isLocalPlayer && localCrouching) || crouching);
    }

    //void OnCollisionStay(Collision collision)
    //{
    //    if (isLocalPlayer)
    //    {
    //        foreach (ContactPoint point in collision.contacts)
    //        {
    //            if (point.thisCollider == GetComponent<SphereCollider>() && point.normal.y > (Quaternion.Euler(maximumSlopeAngle, 0f, 0f) * Vector3.forward).y)
    //            {
    //                grounded = true;
    //                collisionNormal = point.normal;
    //            }
    //        }
    //    }
    //}

    void OnJump()
    {
        transform.FindChild("Model").GetComponent<Animator>().SetTrigger("Jump");
    }

    void OnAmmoRefill()
    {
        transform.FindChild("Model").GetComponent<Animator>().SetTrigger("Reload");
    }

    [Command]
    void CmdSetSpeed(float speed)
    {
        this.speed = speed;
    }

    [Command]
    void CmdJump()
    {
        EventOnJump();
    }

    [Command]
    void CmdAmmoRefill()
    {
        EventOnAmmoRefill();
    }

    void OnApplicationFocus(bool focus)
    {
        if (isLocalPlayer)
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

    [Command]
    void CmdSetCrouch(bool crouching)
    {
        this.crouching = crouching;
    }

    void OnTriggerStay(Collider col)
    {
        if(col.gameObject.tag == "Ammo")
        {
            if(!isLocalPlayer)
            {
                return;
            }
            if(Input.GetButton("Reload") && gun.ammo < 30)
            {
                print("Reloading");
                gun.AmmoRefill();
                CmdAmmoRefill();
            }
        }
    }

    void GroundCheck()
    {
        RaycastHit hitInfo;
        if(Physics.SphereCast(transform.position, GetComponent<CapsuleCollider>().radius - 0.01f, Vector3.down, out hitInfo,
                              (GetComponent<CapsuleCollider>().height / 2f) - GetComponent<CapsuleCollider>().radius + 0.02f,
                              ~(Physics.AllLayers & (1 << LayerMask.NameToLayer("Player"))),
                              QueryTriggerInteraction.Ignore) && hitInfo.normal.y > (Quaternion.Euler(maximumSlopeAngle, 0f, 0f) * Vector3.forward).y)
        {
            grounded = true;
            collisionNormal = hitInfo.normal;
            Debug.Log(hitInfo.transform.name);
        }
        else
        {
            grounded = false;
        }
    }
}
