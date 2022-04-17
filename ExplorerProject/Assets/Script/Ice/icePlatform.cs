using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class icePlatform : MonoBehaviour
{
    [SerializeField] private Material Freeze;
    [SerializeField] private Material UnFreeze;
    [SerializeField] private float freezeTime = 5.0f;
    private bool isStartCoroutine;
    private bool isFreeze;
    private bool isMelting;

    public WaterLogic onWater;
    [Range(1, 100)] [SerializeField] private float flowSpeed;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(isMelting)
        {
            blinking();
        }

        flowing();
    }
    private IEnumerator blink(float waitTime)
    {
        float endTime = Time.time + waitTime;
        while (Time.time < endTime)
        {
            Debug.Log("in blink coroutine");
            gameObject.GetComponent<Renderer>().material = UnFreeze;
            yield return new WaitForSeconds(0.2f);
            gameObject.GetComponent<Renderer>().material = Freeze;
            yield return new WaitForSeconds(0.2f);
        }
        isStartCoroutine = false;
    }

    void blinking()
    {
        freezeTime -= Time.deltaTime;
        if (isStartCoroutine == false)
        {
            gameObject.GetComponent<Renderer>().material = Freeze;
        }

        if (freezeTime <= 2.0f && freezeTime > 0.0f && isStartCoroutine == false)
        {
            //  Debug.Log("in blink");
            isStartCoroutine = true;
            StartCoroutine(blink(2.0f));
        }
        else if (freezeTime <= 0)
        {
            if(gameObject.transform.childCount != 0 && gameObject.transform.GetChild(0).CompareTag("Player"))
            {
                gameObject.transform.GetChild(0).parent = null;
            }
            Destroy(this.gameObject);
        }
    }

    void flowing()
    {
        if(onWater != null)
        {
            if (onWater.flowDirection == WaterLogic.direction.Forward)
            {
                transform.position += transform.forward * flowSpeed * Time.deltaTime;
            }
            else if (onWater.flowDirection == WaterLogic.direction.Backward)
            {
                transform.position -= transform.forward * flowSpeed * Time.deltaTime;
            }
            else if (onWater.flowDirection == WaterLogic.direction.Right)
            {
                transform.position += transform.right * flowSpeed * Time.deltaTime;
            }
            else if (onWater.flowDirection == WaterLogic.direction.Left)
            {
                transform.position -= transform.right * flowSpeed * Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter(Collider trigEnter)
    {
        if(trigEnter.CompareTag("Water"))
        {
            onWater = trigEnter.transform.GetComponent<WaterLogic>();
        }
        if(trigEnter.CompareTag("sunLight"))
        {
            isMelting = true;
        }
    }
}
