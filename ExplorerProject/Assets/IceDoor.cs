using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceDoor : MonoBehaviour
{

    [SerializeField] private Transform maxOpen;
    [SerializeField] private Transform minOpen;
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float Timer = 5f;
    bool moveAble;
    bool opening;
    // Start is called before the first frame update
    void Start()
    {
        opening = false;
        moveAble = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(moveAble == false)
        {
            if(Timer > 0)
            {
                Timer -= Time.deltaTime;
            }
            else
            {
                moveAble = true;
            }
        }

        if (moveAble == true)
        {   
            if (opening == false)
            {
                if (this.transform.position.x < maxOpen.position.x)
                {
                    this.transform.Translate(movementSpeed * Time.deltaTime, 0f, 0f);
                    if (this.transform.position.x >= maxOpen.position.x)
                    {
                        opening = true;
                    }
                }
            }
            else
            {
                if (opening == true)
                {
                    if (this.transform.position.x > minOpen.position.x)
                    {
                        this.transform.Translate(-movementSpeed * Time.deltaTime, 0f, 0f);
                    }
                    if (this.transform.position.x <= minOpen.position.x)
                    {
                        opening = false;
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
       /* if (other.gameObject.CompareTag("Ice"))
        {
            Timer = 5.0f;
            moveAble = false;
        }*/
    }

  
}
