using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterLogic : MonoBehaviour
{
    public enum direction { None, Forward, Backward, Left, Right };

    public direction flowDirection;

    [SerializeField] private GameObject waterPrefab;

    private float checkDelay = 1.0f;

    [SerializeField] private bool checkFlowing;

    private bool downBlocked;
    private bool forwardBlocked;
    private bool backwardBlocked;
    private bool rightBlocked;
    private bool leftBlocked;

    [SerializeField] private bool Test;

    public int waterValue;

    private WaterLogic otherWater;

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
        GameObject waterSpawn = Instantiate(waterReference, waterPosition, Quaternion.identity, this.gameObject.transform.parent);
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
                                if (forwardHit.transform.tag == "Platform")
                                {
                                    forwardBlocked = true;
                                }

                                if (forwardHit.transform.parent != this.gameObject.transform.parent)
                                {
                                    if (forwardHit.transform.TryGetComponent<WaterLogic>(out WaterLogic forwardWater))
                                    {
                                        if (forwardWater.waterValue < waterValue + 1)
                                        {
                                            if (otherWater != forwardWater)
                                            {
                                                forwardWater.waterValue += 1;
                                                otherWater = forwardWater;
                                            }
                                        }
                                        else
                                        {

                                        }
                                    }
                                }
                                else
                                {
                                    if (forwardHit.transform.TryGetComponent<WaterLogic>(out WaterLogic forwardWater))
                                    {
                                        if (forwardWater.waterValue < waterValue)
                                        {
                                            forwardWater.waterValue = waterValue;
                                        }
                                        else
                                        {

                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            StartCoroutine(WaterGenarating(waterPrefab, transform.position + new Vector3(0, 0, 5)));
                            flowDirection = direction.Forward;
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
                                if (backwardHit.transform.tag == "Platform")
                                {
                                    backwardBlocked = true;
                                }

                                if (backwardHit.transform.parent != this.gameObject.transform.parent)
                                {
                                    if (backwardHit.transform.TryGetComponent<WaterLogic>(out WaterLogic backwardWater))
                                    {
                                        if (backwardWater.waterValue < waterValue + 1)
                                        {
                                            if (otherWater != backwardWater)
                                            {
                                                backwardWater.waterValue += 1;
                                                otherWater = backwardWater;
                                            }
                                        }
                                        else
                                        {

                                        }
                                    }
                                }
                                else
                                {
                                    if (backwardHit.transform.TryGetComponent<WaterLogic>(out WaterLogic backwardWater))
                                    {
                                        if (backwardWater.waterValue < waterValue)
                                        {
                                            backwardWater.waterValue = waterValue;
                                        }
                                        else
                                        {

                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            StartCoroutine(WaterGenarating(waterPrefab, transform.position + new Vector3(0, 0, -5)));
                            flowDirection = direction.Backward;
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
                                if (rightHit.transform.tag == "Platform")
                                {
                                    rightBlocked = true;
                                }

                                if (rightHit.transform.parent != this.gameObject.transform.parent)
                                {
                                    if (rightHit.transform.TryGetComponent<WaterLogic>(out WaterLogic rightWater))
                                    {
                                        if (rightWater.waterValue < waterValue + 1)
                                        {
                                            if (otherWater != rightWater)
                                            {
                                                rightWater.waterValue += 1;
                                                otherWater = rightWater;
                                            }
                                        }
                                        else
                                        {

                                        }
                                    }
                                }
                                else
                                {
                                    if (rightHit.transform.TryGetComponent<WaterLogic>(out WaterLogic rightWater))
                                    {
                                        if (rightWater.waterValue < waterValue)
                                        {
                                            rightWater.waterValue = waterValue;
                                        }
                                        else
                                        {

                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            StartCoroutine(WaterGenarating(waterPrefab, transform.position + new Vector3(5, 0, 0)));
                            flowDirection = direction.Right;
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
                                if (leftHit.transform.tag == "Platform")
                                {
                                    leftBlocked = true;
                                }

                                if (leftHit.transform.parent != this.gameObject.transform.parent)
                                {
                                    if (leftHit.transform.TryGetComponent<WaterLogic>(out WaterLogic leftWater))
                                    {
                                        if (leftWater.waterValue < waterValue + 1)
                                        {
                                            if (otherWater != leftWater)
                                            {
                                                leftWater.waterValue += 1;
                                                otherWater = leftWater;
                                            }
                                        }
                                        else
                                        {

                                        }
                                    }
                                }
                                else
                                {
                                    if (leftHit.transform.TryGetComponent<WaterLogic>(out WaterLogic leftWater))
                                    {
                                        if (leftWater.waterValue < waterValue)
                                        {
                                            leftWater.waterValue = waterValue;
                                        }
                                        else
                                        {

                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            StartCoroutine(WaterGenarating(waterPrefab, transform.position + new Vector3(-5, 0, 0)));
                            flowDirection = direction.Left;
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
