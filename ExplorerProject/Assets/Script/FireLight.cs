using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLight : interactableObject
{
    private Light fire;
    // Start is called before the first frame update
    void Start()
    {
        fire = this.GetComponentInChildren<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (other.gameObject.CompareTag("Fire"))
        {
           fire.enabled = true;
        }
    }
}
