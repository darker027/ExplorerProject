using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterLogic : MonoBehaviour
{
    public enum direction { None, Forward, Backward, Left, Right };

    public direction flowDirection;

    [SerializeField] private GameObject waterPrefab;

    private float checkDelay;

    [SerializeField] private bool checkFlowing;

    private bool downBlocked;
    private bool forwardBlocked;
    private bool backwardBlocked;
    private bool rightBlocked;
    private bool leftBlocked;
    [SerializeField] private bool Test;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        
            NormalMode();
       

     
    }

    IEnumerator WaterGenarating(GameObject waterReference, Vector3 waterPosition)
    {
        yield return new WaitForSeconds(0.5f);
        GameObject waterSpawn = Instantiate(waterReference, waterPosition, Quaternion.identity);
    }

    private void NormalMode()
    {
        if (!checkFlowing)
        {
            if (!downBlocked && checkDelay <= 0)
            {
                // Checking flowing downward
                if (Physics.Raycast(gameObject.transform.position, -transform.up, out RaycastHit downHit, 1.25f))
                {
                    // Checking flowing forward
                    if (!forwardBlocked)
                    {
                        if (Physics.Raycast(gameObject.transform.position, transform.forward, out RaycastHit forwardHit, 2.5f))
                        {
                            if (forwardHit.transform.tag == "Water" || forwardHit.transform.tag == "Platform")
                            {
                                forwardBlocked = true;
                            }
                        }
                        else
                        {
                            StartCoroutine(WaterGenarating(waterPrefab, transform.position + new Vector3(0, 0, 5)));
                            flowDirection = direction.Forward;
                            forwardBlocked = true;
                            checkDelay = 1.0f;
                        }
                    }

                    // Checking flowing backward
                    if (!backwardBlocked)
                    {
                        if (Physics.Raycast(gameObject.transform.position, -transform.forward, out RaycastHit backwardHit, 2.5f))
                        {
                            if (backwardHit.transform.tag == "Water" || backwardHit.transform.tag == "Platform")
                            {
                                backwardBlocked = true;
                            }
                        }
                        else
                        {
                            StartCoroutine(WaterGenarating(waterPrefab, transform.position + new Vector3(0, 0, -5)));
                            flowDirection = direction.Backward;
                            backwardBlocked = true;
                            checkDelay = 1.0f;
                        }
                    }

                    // Checking flowing right
                    if (!rightBlocked)
                    {
                        if (Physics.Raycast(gameObject.transform.position, transform.right, out RaycastHit rightHit, 2.5f))
                        {
                            if (rightHit.transform.tag == "Water" || rightHit.transform.tag == "Platform")
                            {
                                rightBlocked = true;
                            }
                        }
                        else
                        {
                            StartCoroutine(WaterGenarating(waterPrefab, transform.position + new Vector3(5, 0, 0)));
                            flowDirection = direction.Right;
                            rightBlocked = true;
                            checkDelay = 1.0f;
                        }
                    }

                    // Checking flowing left
                    if (!leftBlocked)
                    {
                        if (Physics.Raycast(gameObject.transform.position, -transform.right, out RaycastHit leftHit, 2.5f))
                        {
                            if (leftHit.transform.tag == "Water" || leftHit.transform.tag == "Platform")
                            {
                                leftBlocked = true;
                            }
                        }
                        else
                        {
                            StartCoroutine(WaterGenarating(waterPrefab, transform.position + new Vector3(-5, 0, 0)));
                            flowDirection = direction.Left;
                            leftBlocked = true;
                            checkDelay = 1.0f;
                        }
                    }

                    if (forwardBlocked && backwardBlocked && rightBlocked && leftBlocked)
                    {
                        downBlocked = true;
                        return;
                    }
                }
                else
                {
                    StartCoroutine(WaterGenarating(waterPrefab, transform.position + new Vector3(0, -2.5f, 0)));
                    flowDirection = direction.None;
                    downBlocked = true;
                    checkDelay = 1.0f;
                    return;
                }
            }
            else
            {
                checkDelay -= Time.deltaTime;
            }
        }
    }

 
}
