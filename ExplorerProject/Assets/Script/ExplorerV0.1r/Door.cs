using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Material lockDoor;
    [SerializeField] private Material unlockDoor;
    [SerializeField] private Light doorLight;

    private GameObject[] buttons;
    private bool allButtonCheck;

    void Start()
    {
        buttons = GameObject.FindGameObjectsWithTag("Button");
    }

    // Update is called once per frame
    void Update()
    {
        for (int index = 0; index < buttons.Length; index++)
        {
            if (buttons[index].GetComponent<Button>().buttonTrig == true)
            {
                allButtonCheck = true;
            }
            else
            {
                allButtonCheck = false;
                break;
            }
        }

        if (allButtonCheck)
        {
            doorLight.enabled = true;
            gameObject.GetComponent<Renderer>().material = unlockDoor;
        }
        else
        {
            doorLight.enabled = false;
            gameObject.GetComponent<Renderer>().material = lockDoor;
        }
    }
}
