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
    // Start is called before the first frame update
    void Start()
    {
        havePower = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(electricValue > 0 && havePower == true)
        {
           
            electricValue -= Time.deltaTime; 
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
         if (other.gameObject.CompareTag("Electric"))
         {
            //electricValue = other.GetComponent<ElectricAbility>().electric;
           // Debug.Log(other.GetComponent<ElectricAbility>().electric + "switch");
        }
    }
    public void setValue(float value)
    {
        electricValue = value;
        havePower = true;
    }
}
