using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class icespinplatform : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 5f;
    [SerializeField] private float Timer = 5f;
    private bool isFreeze;
    // Start is called before the first frame update
    void Start()
    {
        isFreeze = false;
    }

    // Update is called once per frame
    void Update()
    {
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
        if (isFreeze == false)
        {
            this.transform.Rotate(0f, rotateSpeed * Time.deltaTime, 0f);
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
