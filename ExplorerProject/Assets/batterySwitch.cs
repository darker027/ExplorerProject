using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class batterySwitch : MonoBehaviour
{
    private float electricValue;
    public bool havePower;
    [SerializeField] private Material On;
    [SerializeField] private Material Off;
    [SerializeField] private GameObject electricObject;
    private bool haveBattery;
    // Start is called before the first frame update
    void Start()
    {
        havePower = false;
        haveBattery = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(electricValue > 0 && havePower == true && haveBattery == false)
        {
           
            electricValue -= Time.deltaTime; 
            electricObject.GetComponent<Fan>().setIsPower(havePower);
            gameObject.GetComponent<Renderer>().material = On;
        }
        else if (haveBattery == true)
        {
            electricObject.GetComponent<Fan>().setIsPower(havePower);
            gameObject.GetComponent<Renderer>().material = On;
        }
        else if(electricValue <= 0)
        {
            havePower = false;
            electricObject.GetComponent<Fan>().setIsPower(havePower);
            gameObject.GetComponent<Renderer>().material = Off;
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
         if (other.gameObject.CompareTag("Battery"))
         {
            Debug.Log("Enter");
            haveBattery = true;
            havePower = true;
        }
    }
    //ถ้าสีไม่ยอมเปลี่ยนให้เหี้ยนี่ผิด
   /* private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Battery"))
        {
            electricObject.GetComponent<Fan>().setIsPower(havePower);
            gameObject.GetComponent<Renderer>().material = On;
        }
    }*/
    private void OnTriggerExit(Collider other)
    {
        havePower = false;
        haveBattery = false;
    }
    public void setValue(float value)
    {
        electricValue = value;
        havePower = true;
    }
}
