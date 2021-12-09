using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class icespinplatform : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 5f;
    [SerializeField] private float Timer = 5f;
    [SerializeField] private waterWheelSwitch waterSwitch;
    private bool isFreeze;
    private bool IsSwitch;
    // Start is called before the first frame update
    void Start()
    {
        isFreeze = false;
        IsSwitch = false;
    }

    // Update is called once per frame
    void Update()
    {
        IsSwitch = waterSwitch.IsOn;
        if (isFreeze == true)
        {
            if (Timer > 0)
            {
                Timer -= Time.deltaTime;
            }
            else
            {
                isFreeze = false;
            }
        }
        if (isFreeze == false && IsSwitch == true)
        {
            this.transform.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
         if (other.gameObject.CompareTag("Ice"))
         {
             Timer = 5.0f;
             isFreeze = true;
         }
    }
}
