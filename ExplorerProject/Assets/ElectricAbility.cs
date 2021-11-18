using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricAbility : Ability
{
    public float electricValue;
    // Start is called before the first frame update
    void Start()
    {
        electricValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void OnTriggerEnter(Collider collision)
    {
        base.OnTriggerEnter(collision);

    }
    public void setValue(float value)
    {
        electricValue = value;
    }
    public float getValue()
    {
        return electricValue;
    }
}
