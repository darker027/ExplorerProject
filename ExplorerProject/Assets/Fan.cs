using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    [SerializeField] private GameObject air;
    [SerializeField] private Material On;
    [SerializeField] private Material Off;
    private bool isPower;
    // Start is called before the first frame update
    void Start()
    {
        isPower = false;
        air.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(isPower == true)
        {
            air.SetActive(true);
            gameObject.GetComponent<Renderer>().material = On;
        }
        else
        {
            air.SetActive(false);
            gameObject.GetComponent<Renderer>().material = Off;
        }
    }
    public void setIsPower(bool power)
    {
        isPower = power;
    }
}
