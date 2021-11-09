using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class icelever : MonoBehaviour
{
    [SerializeField] private float maxTimer = 5f;
    [SerializeField] private Material green;
    [SerializeField] private Material red;
    [SerializeField] private Material freeze;

    public bool isOn;
    private bool isPlayerOn;
    private float Timer;
    private float leverTimer;
    private float CurrentTimer;
    private bool isFreeze;
    // Start is called before the first frame update
    void Start()
    {
        isOn = false;
        isFreeze = false;
        Timer = maxTimer;
        leverTimer = 0.0f;
        gameObject.GetComponent<Renderer>().material = red;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isPlayerOn == true)
        {
            if(isOn == false && isFreeze == false && leverTimer <= 0.0f)
            {
                isOn = true;
                leverTimer = maxTimer;
                gameObject.GetComponent<Renderer>().material = green;
            }
        }
        if(isOn == true && isFreeze == false)
        {
            if(leverTimer > 0)
            {
                leverTimer -= Time.deltaTime;
            }
            else
            {
                isOn = false;
                gameObject.GetComponent<Renderer>().material = red;
            }
        }
        if (isFreeze == true)
        {
            if (Timer > 0)
            {
                Timer -= Time.deltaTime;
            }
            else
            {
                isFreeze = false;
                if (isOn == false)
                {
                    gameObject.GetComponent<Renderer>().material = red;
                }
                else if(isOn == true){
                    gameObject.GetComponent<Renderer>().material = green;
                }
            }
        }
      
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerOn = true;
        }
        if (other.gameObject.CompareTag("Ice") && isFreeze == false)
        {
            Timer = maxTimer;
            isFreeze = true;
            gameObject.GetComponent<Renderer>().material = freeze;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        isPlayerOn = false;
    }
}
