using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private Material buttonDefault;
    [SerializeField] private Material buttonColor;

    [SerializeField] private PlayerCharacterController playerSC;

    private GameObject[] buttons;

    public bool buttonTrig;
    private Vector3 defaultPos;
    private Vector3 triggedPos;

    private void Start()
    {
        buttons = GameObject.FindGameObjectsWithTag("Button");
        defaultPos = gameObject.transform.position;
        triggedPos = new Vector3(gameObject.transform.position.x, 0.1f, gameObject.transform.position.z);
    }

    private void Update()
    {
        if(buttonTrig)
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
            gameObject.GetComponent<Renderer>().material = buttonDefault;
            transform.position = Vector3.MoveTowards(gameObject.transform.position, triggedPos, 1 * Time.deltaTime);
        }
        else
        {
            gameObject.GetComponent<BoxCollider>().enabled = true;
            gameObject.GetComponent<Renderer>().material = buttonColor;
            transform.position = Vector3.MoveTowards(gameObject.transform.position, defaultPos, 1 * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(playerSC.buttonOrder[playerSC.buttonNumber] == this.gameObject)
            {
                buttonTrig = true;
                playerSC.buttonNumber += 1;
            }
            else
            {
                for(int index = 0; index < buttons.Length; index++)
                {
                    buttons[index].GetComponent<Button>().buttonTrig = false;
                }
                playerSC.buttonNumber = 0;
            }
        }
    }
}
