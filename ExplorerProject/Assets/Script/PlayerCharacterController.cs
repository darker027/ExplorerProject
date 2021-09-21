using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerCharacterController : MonoBehaviour
{
    CharacterController charControl;

    public bool grounded = true;
    public float speed = 1f;
    public float jumpHeight = 1f;
    private float gravityValue = -9.81f;

    public float InputMagnitude;

    Vector3 moveDirection;
    Vector3 CCvelocity;

    // - - - - - - puzzle test - - - - - -
    // This may be removed, if it is not be used for the puzzle in future.
    public GameObject[] buttonOrder;
    public int buttonNumber;

    // - - - - - - - - - - - - - - - - - - - - - - - - -
    void Start()
    {
        charControl = GetComponent<CharacterController>();
    }

    void Update()
    {
        grounded = charControl.isGrounded;

        if (grounded && CCvelocity.y < 0)
        {
            CCvelocity.y = 0f;
        }

        moveDirection.x = Input.GetAxis("Horizontal");
        moveDirection.z = Input.GetAxis("Vertical");

        moveDirection = CameraController.Instance.transform.TransformDirection(moveDirection);
        moveDirection.y = 0;

        moveDirection.Normalize();
        InputMagnitude = moveDirection.magnitude;

        charControl.Move(moveDirection * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && grounded)
        {
            Debug.Log("Jump!");
            CCvelocity.y = Mathf.Sqrt(2 * jumpHeight * 9.81f);
        }

        CCvelocity.y += gravityValue * Time.deltaTime;

        charControl.Move(CCvelocity * Time.deltaTime);
    }
}
