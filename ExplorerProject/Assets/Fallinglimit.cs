using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fallinglimit : MonoBehaviour
{
    private void OnTriggerExit(Collider trigExit)
    {
        if(trigExit.CompareTag("Player"))
        {
            Destroy(trigExit.gameObject, 1.5f);
        }
    }
}
