using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [Header("Objects Reference")]
    [SerializeField] private LayerMask GroundMask;
    [SerializeField] private Transform CameraBase;
    [SerializeField] private Transform PlayerFeet;

    private Rigidbody playerRigid;

    private float playerHeight;

    [Header("Character Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float movementMultiplier;
    [SerializeField] private float aerialMultiplier;

    [SerializeField] private float stepHeight;

    private float horizontalMovement;
    private float verticalMovement;

    private Vector3 movementDirection;
    private Vector3 slopeDirection;
    private Vector3 Movement;

    private float stamina = 10f;
    public float playerStamina => stamina;
    private float cooldownTime = 5f;
    public float playerCDTime => cooldownTime;
    private bool exhausted;
    public bool Exhaust => exhausted;

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
    //[SerializeField] private Vector3 platformVelocity;

    //[SerializeField] private Vector3 feetPos;
    //[SerializeField] private Vector3 feetFor;


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
        onGrounded = Physics.CheckSphere(PlayerFeet.transform.position, 0.25f, GroundMask);
        

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
        slopeDirection = Vector3.ProjectOnPlane(movementDirection, slopeHit.normal);
        CharacterMovement();
    }

    void CharacterMovement()
    {
        if (onGrounded)
        {
            if (!onSlope())
                StairStep();

            if (Input.GetKey(KeyCode.LeftShift) && stamina > 0 && !exhausted && movementDirection != Vector3.zero)
            {
                playerRigid.AddForce(movementDirection.normalized * runSpeed * movementMultiplier, ForceMode.Acceleration);
                stamina -= 2 * Time.deltaTime;
                stamina = Mathf.Clamp(stamina, 0, 10);
            }
            else
            {
                if (exhausted)
                {
                    playerRigid.AddForce(movementDirection.normalized * (moveSpeed - 1) * movementMultiplier, ForceMode.Acceleration);
                }
                else
                {
                    playerRigid.AddForce(movementDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
                }

                if (stamina > 0 && stamina < 10)
                {
                    stamina += Time.deltaTime;
                    stamina = Mathf.Clamp(stamina, 0, 10);
                }
                else if (stamina <= 0)
                {
                    cooldownTime -= Time.deltaTime;
                    exhausted = true;

                    if (cooldownTime < 0)
                    {
                        stamina += Time.deltaTime;
                        stamina = Mathf.Clamp(stamina, 0, 10);
                        exhausted = false;
                        cooldownTime = 5.0f;
                    }
                }
            }
        }
        else if (onGrounded && onSlope())
        {
            playerRigid.AddForce(slopeDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        //else if (onGrounded && onMovingPlatform)
        //{
        //    playerRigid.velocity = new Vector3((playerRigid.velocity.x + platformRigid.velocity.x), playerRigid.velocity.y, (playerRigid.velocity.z + platformRigid.velocity.z));
        //}
        else if (!onGrounded)
        {
            playerRigid.AddForce(movementDirection.normalized * moveSpeed * movementMultiplier * aerialMultiplier, ForceMode.Acceleration);
        } 

        Xvelocity = playerRigid.velocity.x;
        Yvelocity = playerRigid.velocity.y;
        Zvelocity = playerRigid.velocity.z;

    }

    void StairStep()
    {
        if (movementDirection != Vector3.zero)
        {
            RaycastHit hitLower;
            if (Physics.Raycast(PlayerFeet.position, transform.TransformDirection(PlayerFeet.forward), out hitLower, 0.5f, GroundMask))
            {
                Debug.Log(hitLower.transform.name);

                RaycastHit hitUpper;
                if (!Physics.Raycast(new Vector3(PlayerFeet.position.x, PlayerFeet.position.y + stepHeight, PlayerFeet.position.z), transform.TransformDirection(PlayerFeet.forward), out hitUpper, 0.6f, GroundMask))
                {
                    playerRigid.position += new Vector3(0f, 0.1f, 0f);
                }
            }

            RaycastHit hitLowerR45;
            if (Physics.Raycast(PlayerFeet.position, transform.TransformDirection(1.5f, 0, 1), out hitLowerR45, 0.5f, GroundMask))
            {
                RaycastHit hitUpperR45;
                if (!Physics.Raycast(new Vector3(PlayerFeet.position.x, PlayerFeet.position.y + stepHeight, PlayerFeet.position.z), transform.TransformDirection(1.5f, 0, 1), out hitUpperR45, 0.6f, GroundMask))
                {
                    playerRigid.position += new Vector3(0f, 0.1f, 0f);
                }
            }

            RaycastHit hitLowerL45;
            if (Physics.Raycast(PlayerFeet.position, transform.TransformDirection(-1.5f, 0, 1), out hitLowerL45, 0.5f, GroundMask))
            {
                RaycastHit hitUpperL45;
                if (!Physics.Raycast(new Vector3(PlayerFeet.position.x, PlayerFeet.position.y + stepHeight, PlayerFeet.position.z), transform.TransformDirection(-1.5f, 0, 1), out hitUpperL45, 0.6f, GroundMask))
                {
                    playerRigid.position += new Vector3(0f, 0.1f, 0f);
                }
            }
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
            //onMovingPlatform = true;
           // this.gameObject.transform.parent = colEnter.transform;
            //platformRigid = colEnter.gameObject.GetComponent<Rigidbody>();

        }
    }

    private void OnCollisionExit(Collision colExit)
    {
        if (colExit.gameObject.CompareTag("MovingPlatform"))
        {
            //onMovingPlatform = false;
            //platformRigid = colEnter.gameObject.GetComponent<Rigidbody>();
           // gameObject.transform.parent = null;
        }
    }

    public void setSwitchMovement(bool x)
    {
        isClickSwitch = x;
    }
}
