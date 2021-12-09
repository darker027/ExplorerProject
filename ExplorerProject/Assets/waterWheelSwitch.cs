using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterWheelSwitch : MonoBehaviour
{
    public bool IsOn;
    // Start is called before the first frame update
    void Start()
    {
        IsOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Battery"))
        {
            IsOn = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Battery"))
        {
            IsOn = false;
        }
    }
}
