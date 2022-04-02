using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeepPuddle : MonoBehaviour
{
    [SerializeField] private Material Freeze;
    [SerializeField] private Material UnFreeze;
    [SerializeField] private float freezeTime;
    [SerializeField] private BoxCollider deepCollider;
    [SerializeField] private GameObject icePlatform;
    private bool isStartCoroutine;
    private bool IsFreeze;
    void Start()
    {
        gameObject.GetComponent<Renderer>().material = UnFreeze;
        IsFreeze = false;
        isStartCoroutine = false;
     
    }

    // Update is called once per frame
    void Update()
    {
        platform();
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && IsFreeze == false)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (other.CompareTag("Ice") && IsFreeze == false)
        {
            /* freezeTime = 5;
             deepCollider.enabled = !deepCollider.enabled;
             IsFreeze = true;*/
            IsFreeze = true;
            Instantiate(icePlatform, new Vector3(other.transform.position.x, other.transform.position.y+ 0.5f , other.transform.position.z), Quaternion.identity);
        }
    }
    void platform()
    {
        if (IsFreeze)
        {
           freezeTime -= Time.deltaTime;
           if (freezeTime <= 0)
            {
           
                IsFreeze = false;
            }
        }
    }
   //old method for freeze water
    void freezeBox()
    {
        if (IsFreeze)
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
                deepCollider.enabled = !deepCollider.enabled;
                IsFreeze = false;
            }
        }
        else
        {
            gameObject.GetComponent<Renderer>().material = UnFreeze;
        }
    }
}
