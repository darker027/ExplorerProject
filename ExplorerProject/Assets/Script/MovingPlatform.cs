using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Waypoint Setup")]
    [SerializeField] private Transform[] WayPoints;

    private Vector3 currentPos;
    private Vector3 targetPos;

    private int orderPoint;
    private bool nextPoint;

    [Header("Movement Setup")]
    [Range(0.01f, 0.1f)]
    [SerializeField] private float moveSpeed;

    [Header("Material Setup")]
    [SerializeField] private Material Freezing;
    [SerializeField] private Material UnFreezing;

    [Header("Behavior Setup")]
    [Range(1, 5)]
    [SerializeField] private float freezeTime;

    private float freezeCountdown;
    private bool isFreezing;

    // - - - - - - - - - - - - - - - - - - - -
    void Start()
    {
        targetPos = WayPoints[orderPoint].transform.position;
    }

    void Update()
    {
        currentPos = gameObject.transform.position;

        if(isFreezing)
        {
            //Platform is froze!
            freezeCountdown -= Time.deltaTime;
            gameObject.GetComponent<Renderer>().material = Freezing;

            if(freezeCountdown <= 0)
            {
                gameObject.GetComponent<Renderer>().material = UnFreezing;
                isFreezing = false;
            }
        }
        else
        {
            gameObject.transform.position = Vector3.MoveTowards(currentPos, targetPos, moveSpeed);
        }

        if (currentPos == targetPos && !nextPoint)
        {
            StartCoroutine(NextPoint());
        }
    }

    private IEnumerator NextPoint()
    {
        if(orderPoint < (WayPoints.Length - 1))
        {
            orderPoint += 1;
            targetPos = WayPoints[orderPoint].transform.position;

            nextPoint = true;
        }
        else
        {
            orderPoint = 0;
            targetPos = WayPoints[orderPoint].transform.position;

            nextPoint = true;
        }

        yield return new WaitForSeconds(0.5f);
        nextPoint = false;
    }

    public void FreezePlatform()
    {
        freezeCountdown = freezeTime;
        isFreezing = true;
    }

    private void OnTriggerEnter(Collider trigEnter)
    {
        if(trigEnter.CompareTag("Player"))
        {
            trigEnter.gameObject.transform.parent = this.transform;
        }
        if (trigEnter.gameObject.CompareTag("Ice"))
        {
            FreezePlatform();
        }
    }

    private void OnTriggerExit(Collider trigExit)
    {
        if (trigExit.CompareTag("Player"))
        {
            trigExit.gameObject.transform.parent = null;
        }
    }
}
