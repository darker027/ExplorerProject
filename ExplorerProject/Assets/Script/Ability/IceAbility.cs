using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceAbility : Ability
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void OnTriggerEnter(Collider collision)
    {
        base.OnTriggerEnter(collision);

        if (collision.gameObject.CompareTag("FireDoor"))
        {
            destroyBullet();
        }
        if (collision.gameObject.CompareTag("FireLight"))
        {
            destroyBullet();
        }
        if (collision.gameObject.CompareTag("IceDoor"))
        {
            destroyBullet();
        }
    }
}
