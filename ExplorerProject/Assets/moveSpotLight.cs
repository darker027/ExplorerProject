using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveSpotLight : MonoBehaviour
{
    private bool isClickSwitch;
    private Vector3 inputMovement;
    [SerializeField] private int moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        isClickSwitch = false;

       
    }

    // Update is called once per frame
    void Update()
    {
        if(isClickSwitch == false)
        {
            return;
        }
        movement();
        
    }

    private void movement()
    {
        inputMovement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        transform.Translate(inputMovement * Time.deltaTime * moveSpeed, Space.World);
    }
    public void setSwitchSpotlight(bool x)
    {
        isClickSwitch = x;
    }
}
