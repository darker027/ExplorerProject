using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricAbility : Ability
{
    public float electric;
    // Start is called before the first frame update
    void Start()
    {
        //electric = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(electric);
    }
    public override void OnTriggerEnter(Collider collision)
    {
        base.OnTriggerEnter(collision);
        if (collision.gameObject.CompareTag("ElectricSwitch"))
        {
            collision.GetComponent<batterySwitch>().setValue(electric);
            destroyBullet();
        }

    }
    public void setValue(float value)
    {
        electric = value;
    }
    public float getValue()
    {
        return electric;
    }
}
