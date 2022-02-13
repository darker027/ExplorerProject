using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateOpenLight : MonoBehaviour
{
    [SerializeField] private List<Light> lightGuidePath;

    private bool turnOnLight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(turnOnLight)
        {
            for(int index = 0; index < lightGuidePath.Count; index++)
            {
                lightGuidePath[index].intensity = 75;
            }
        }
        else
        {
            for(int index = 0; index < lightGuidePath.Count; index++)
            {
                if(lightGuidePath[index].intensity > 0)
                {
                    lightGuidePath[index].intensity -= 1;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            turnOnLight = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            turnOnLight = false;
        }
    }
}
