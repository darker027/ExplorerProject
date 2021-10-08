using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDoor : interactableObject
{
    // Start is called before the first frame update
    void Start()
    {
        
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
            this.gameObject.SetActive(false);
        }
    }

}
