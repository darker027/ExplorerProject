using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elevator : MonoBehaviour
{
    [SerializeField] private Transform maxOpen;
    [SerializeField] private Transform minOpen;
    [SerializeField] private icelever IceLever;
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

        if (IceLever.isOn == true)
        {
            if (opening == false)
            {
                if (this.transform.position.y < maxOpen.position.y)
                {
                    this.transform.Translate(0f, movementSpeed * Time.deltaTime, 0f);
                    if (this.transform.position.y >= maxOpen.position.y)
                    {
                        opening = true;
                    }
                }
            }
            else
            {
                if (opening == true)
                {
                    if (this.transform.position.y > minOpen.position.y)
                    {
                        this.transform.Translate(0f, -movementSpeed * Time.deltaTime, 0f);
                    }
                    if (this.transform.position.y <= minOpen.position.y)
                    {
                        opening = false;
                    }
                }
            }
        }
        else if(IceLever.isOn == false && opening == false)
        {
            if (this.transform.position.y > minOpen.position.y)
            {
                this.transform.Translate(0f, -movementSpeed * Time.deltaTime, 0f);
            }
            if (this.transform.position.y <= minOpen.position.y)
            {
                opening = false;
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
