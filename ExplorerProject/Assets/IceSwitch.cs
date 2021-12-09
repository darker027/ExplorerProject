using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSwitch : MonoBehaviour
{
    public bool IsOn;
    public bool IsPlayerNear;
    [SerializeField] Material blue;
    // Start is called before the first frame update
    void Start()
    {
        IsOn = false;
        IsPlayerNear = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E) && IsPlayerNear)
        {
            IsOn = true;
        }
        if(IsOn == true)
        {
            this.gameObject.GetComponent<Renderer>().material = blue;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            IsPlayerNear = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            IsPlayerNear = false;
        }
    }
}
