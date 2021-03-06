using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class icePlatform : MonoBehaviour
{
    [SerializeField] private Material Freeze;
    [SerializeField] private Material UnFreeze;
    [SerializeField] private float freezeTime = 5.0f;
    [SerializeField] private float shrinkingTime = 5.0f;
    private float smoothTime = 5.0f;
    private float velo = 0f;
    private bool isDelay;
    private float timer;
    private bool isStartCoroutine;
    private bool isFreeze;
    private bool isMelting;
    private Rigidbody rigidbody;
    [SerializeField] private float forceMulti; 

    private WaterLogic _Onwater;
    public WaterLogic onWater { get { return _Onwater; } set { _Onwater = value; Debug.Log("set water"); } }
    [Range(1, 100)] [SerializeField] private float flowSpeed;
    private void Awake()
    {
        rigidbody = this.GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        isDelay = false;
        shrinkingEffect(new Vector3(0, 0, 0), shrinkingTime);
    }

    // Update is called once per frame
    void Update()
    {
        if(isMelting)
        {
            shrinkingEffect(new Vector3(0, 0, 0), shrinkingTime);
        }
        if(transform.localScale == new Vector3(0, 0, 0))
        {
            Destroy(this.gameObject);
        }
        
    }
  
    private void shrinkingEffect(Vector3 targetScale, float duration)
    {
        StartCoroutine(ScaleToTargetCoroutine(targetScale, duration));
    }
    private IEnumerator ScaleToTargetCoroutine(Vector3 targetScale, float duration)
    {
        Vector3 startScale = transform.localScale;
        float timer = 0.0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = timer / duration;
            //smoother step algorithm
            t = t * t * t * (t * (6f * t - 15f) + 10f);
            transform.localScale = Vector3.Lerp(startScale, targetScale, t);
            yield return null;
        }

        yield return null;
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

    }

   

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("enter");
        if (collision.gameObject.CompareTag("Water"))
        {

            onWater = collision.transform.GetComponent<WaterLogic>();

        }
        if (collision.gameObject.CompareTag("sunLight"))
        {
            isMelting = true;
        }
    }

    public void addForce(Vector3 force)
    {
        rigidbody.AddForce(force.normalized * forceMulti, ForceMode.Force);
    }

    //private void Move()
    //{
    //    Debug.Log("Move");
    //    if (onWater.flowDirection == WaterLogic.direction.Forward)
    //    {
    //        transform.position -= Vector3.forward * flowSpeed * Time.deltaTime;
    //    }
    //    else if (onWater.flowDirection == WaterLogic.direction.Backward)
    //    {
    //        transform.position += Vector3.forward * flowSpeed * Time.deltaTime;
    //    }
    //    else if (onWater.flowDirection == WaterLogic.direction.Right)
    //    {
    //        transform.position -= Vector3.right * flowSpeed * Time.deltaTime;
    //    }
    //    else if (onWater.flowDirection == WaterLogic.direction.Left)
    //    {
    //        transform.position += Vector3.right * flowSpeed * Time.deltaTime;
    //    }
    //}
}
