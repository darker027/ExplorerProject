using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [Header("Object Reference")]
    [SerializeField] private LayerMask GroundMask;
    [SerializeField] private Transform CameraBase;
    [SerializeField] private Transform PlayerFeet;

    private Rigidbody playerRigid;

    private float playerHeight;

    [Header("Character Movement")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float movementMultiplier;
    [SerializeField] private float aerialMultiplier;

    private float horizontalMovement;
    private float verticalMovement;

    private Vector3 movementDirection;
    private Vector3 slopeDirection;
    private Vector3 Movement;

    [Header("Character Jumping")]
    [SerializeField] private float jumpForce;

    [Header("Character Detection")]
    private bool onGrounded;

    private RaycastHit slopeHit;

    private bool isClickSwitch;
    private bool onSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, (playerHeight / 2) + 0.5f))
        {
            if (slopeHit.normal != Vector3.up)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    private Rigidbody platformRigid;
    private bool onMovingPlatform;



    [Header("Dragging")]
    [SerializeField] private float groundDrag;
    [SerializeField] private float airDrag;

    [Header("Debugging")]
    [SerializeField] private float Xvelocity;
    [SerializeField] private float Yvelocity;
    [SerializeField] private float Zvelocity;
    [SerializeField] private Vector3 platformVelocity;


    private void OnEnable()
    {
        //find camera On load
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    // Start is called before the first frame update
    void Start()
    {
        playerRigid = gameObject.GetComponent<Rigidbody>();
        playerRigid.freezeRotation = true;
        isClickSwitch = false;
        playerHeight = gameObject.GetComponent<CapsuleCollider>().height;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //if (CameraBase == null)
            
            CameraBase = GameObject.FindWithTag("cameraBase").transform;
       
    }
    // Update is called once per frame
    void Update()
    {
        
        
        if (isClickSwitch == true)
        {
            return;
        }
       
        //Character Facing
        gameObject.transform.rotation = Quaternion.Euler(0, CameraBase.eulerAngles.y, 0);

        //Character Movement
        horizontalMovement = Input.GetAxis("Horizontal");
        verticalMovement = Input.GetAxis("Vertical");
        movementDirection = (transform.forward * verticalMovement) + (transform.right * horizontalMovement);
        movementDirection.Normalize();

        slopeDirection = Vector3.ProjectOnPlane(movementDirection, slopeHit.normal);

        //Character Jumping
        onGrounded = Physics.CheckSphere(PlayerFeet.transform.position, 0.15f, GroundMask);
        

        if (Input.GetKeyDown(KeyCode.Space) && onGrounded)
        {
            playerRigid.velocity = new Vector3(playerRigid.velocity.x, 0, playerRigid.velocity.z);
            playerRigid.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            print("Jump!!");
        }

        //Character Dragging
        DragControl();

        
    }

    private void FixedUpdate()
    {
        CharacterMovement();
    }

    void CharacterMovement()
    {
        //Movement = transform.TransformDirection(movementDirection);

        if (onGrounded)
        {
            playerRigid.AddForce(movementDirection.normalized * movementSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else if (onGrounded && onSlope())
        {
            playerRigid.AddForce(slopeDirection.normalized * movementSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else if (onGrounded && onMovingPlatform)
        {
            playerRigid.velocity = new Vector3((playerRigid.velocity.x + platformRigid.velocity.x), playerRigid.velocity.y, (playerRigid.velocity.z + platformRigid.velocity.z));
            //playerRigid.velocity = playerRigid.velocity + platformRigid.velocity;
        }
        else if (!onGrounded)
        {
            playerRigid.AddForce(movementDirection.normalized * movementSpeed * movementMultiplier * aerialMultiplier, ForceMode.Acceleration);
        }

        /*if (onGrounded)
        {
            Movement = transform.TransformDirection(movementDirection) * movementSpeed * groundMultiplier;
        }
        else
        {
            Movement = transform.TransformDirection(movementDirection) * movementSpeed * airMultiplier;
        }

        playerRigid.velocity = new Vector3(Movement.x, playerRigid.velocity.y, Movement.z);*/
        Xvelocity = playerRigid.velocity.x;
        Yvelocity = playerRigid.velocity.y;
        Zvelocity = playerRigid.velocity.z;
        if(platformRigid != null)
        {
            platformVelocity = platformRigid.velocity;
        }
        
    }

    void DragControl()
    {
        if(onGrounded)
        {
            playerRigid.drag = groundDrag;
        }
        else
        {
            playerRigid.drag = airDrag;
        }
    }

    private void OnCollisionEnter(Collision colEnter)
    {
        if(colEnter.gameObject.CompareTag("MovingPlatform"))
        {
            onMovingPlatform = true;
            //platformRigid = colEnter.gameObject.GetComponent<Rigidbody>();
            gameObject.transform.parent = colEnter.transform;
        }
    }

    private void OnCollisionExit(Collision colExit)
    {
        if (colExit.gameObject.CompareTag("MovingPlatform"))
        {
            onMovingPlatform = false;
            //platformRigid = colEnter.gameObject.GetComponent<Rigidbody>();
            gameObject.transform.parent = null;
        }
    }

    public void setSwitchMovement(bool x)
    {
        isClickSwitch = x;
    }

    //private void OnCollisionEnter(Collision Enter)
    //{
    //    if(Enter.gameObject.CompareTag("Platform"))
    //    {
    //        onGrounded = true;
    //    }
    //}

    //private void OnCollisionExit(Collision Exit)
    //{
    //    if(Exit.gameObject.CompareTag("Platform"))
    //    {
    //        onGrounded = false;
    //    }
    //}
}
