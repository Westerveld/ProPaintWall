﻿using UnityEngine;
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
    public float crouchModifier;
    [SyncVar]
    public bool crouching = false;

    private Rigidbody rigidBody;
    private bool grounded = false;
    private Vector3 collisionNormal = Vector3.up;
    private float capsuleHeight;
    private Vector3 characterScale;
    private float cameraPosition;

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
        characterScale = transform.FindChild("Ethan").localScale;
        cameraPosition = firstPersonCamera.transform.localPosition.y + capsuleHeight / 2f;
    }

    void FixedUpdate()
    {
        if (isLocalPlayer)
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

            if (CrossPlatformInputManager.GetButton("Crouch") && (grounded || rigidBody.velocity.y < 0f))
            {
                crouching = true;
                CmdSetCrouch(crouching);
            }
            else
            {
                crouching = false;
                CmdSetCrouch(crouching);
            }

            grounded = false;
        }

        if (crouching)
        {
            GetComponent<CapsuleCollider>().height = capsuleHeight / crouchModifier;
            GetComponent<CapsuleCollider>().center = new Vector3(GetComponent<CapsuleCollider>().center.x,
                                                                 -(capsuleHeight / 2f) / crouchModifier,
                                                                 GetComponent<CapsuleCollider>().center.z);
            transform.FindChild("Ethan").localScale = new Vector3(characterScale.x, characterScale.y / crouchModifier, characterScale.z);
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
            transform.FindChild("Ethan").localScale = new Vector3(characterScale.x, characterScale.y, characterScale.z);
            firstPersonCamera.transform.localPosition = new Vector3(firstPersonCamera.transform.localPosition.x,
                                                                    cameraPosition - (capsuleHeight / 2f),
                                                                    firstPersonCamera.transform.localPosition.z);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (isLocalPlayer)
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
}
