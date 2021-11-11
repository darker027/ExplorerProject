using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Object Reference")]
    [SerializeField] private LayerMask GroundMask;
    [SerializeField] private Transform CameraBase;
    [SerializeField] private Transform PlayerFeet;

    private Rigidbody playerRigid;

    [Header("Character Setting")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    private Vector3 Direction;
    private Vector3 Movement;

    [SerializeField]private bool onGrounded;

    // Start is called before the first frame update
    void Start()
    {
        playerRigid = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.rotation = Quaternion.Euler(0, CameraBase.eulerAngles.y, 0);

        Direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Direction.Normalize();

        CharacterMovement();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(Physics.CheckSphere(PlayerFeet.position, 0.1f, GroundMask))
            {
                playerRigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }

    void CharacterMovement()
    {
        Movement = transform.TransformDirection(Direction) * moveSpeed;

        playerRigid.velocity = new Vector3(Movement.x, playerRigid.velocity.y, Movement.z);
    }

    private void OnCollisionEnter(Collision Enter)
    {
        if(Enter.gameObject.CompareTag("Platform"))
        {
            onGrounded = true;
        }
    }

    private void OnCollisionExit(Collision Exit)
    {
        if(Exit.gameObject.CompareTag("Platform"))
        {
            onGrounded = false;
        }
    }
}
