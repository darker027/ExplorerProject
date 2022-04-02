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
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

        blinking();

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

                Destroy(this.gameObject);
            }
     
       
    }

}
