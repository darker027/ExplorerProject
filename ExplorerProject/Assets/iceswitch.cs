using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iceswitch : MonoBehaviour
{
    [SerializeField] GameObject firstPlatform;
    [SerializeField] GameObject secondPlatform;
    private bool switchOn;
    // Start is called before the first frame update
    void Start()
    {
        secondPlatform.SetActive(false);
        switchOn = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
         if (other.gameObject.CompareTag("Ice") && switchOn == false)
        {
            firstPlatform.SetActive(false);
            secondPlatform.SetActive(true);
            switchOn = true;
        }
         else if(other.gameObject.CompareTag("Ice") && switchOn == true)
        {
            firstPlatform.SetActive(true);
            secondPlatform.SetActive(false);
            switchOn = false;
        }
    }
}
