using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterLogic : MonoBehaviour
{
    public int waterSerial;
    public enum direction { None, Forward, Backward, Left, Right };

    public direction flowDirection;
    
    public enum BlockState { None, Forward, Backward, Left, Right, All };
    public bool othercheck = false;
    [SerializeField] BlockState blockState;

    [SerializeField] LayerMask layertoblock;

    [SerializeField] GameObject hitOBJ;

    [SerializeField] private GameObject waterPrefab;
    public int waterValue;
    WaterLogic otherWater;

    [SerializeField] bool checkallow;

    float Delay = -1f;
    // Start is called before the first frame update
    void Start()
    {

        Time.timeScale = 2;
        Raycheck();
        StartCoroutine(CheckValue());
    }

    // Update is called once per frame

    IEnumerator CheckValue()
    {
        int _instance = waterValue;
        yield return new WaitUntil(() => waterValue != _instance);
        Raycheck();
        StartCoroutine(CheckValue());
    }
    public IEnumerator DelayRayCheck()
    {
        
        yield return new WaitForSeconds(0.5f);
        Raycheck();
      
    }
    public void startDelayRayCheck()
    {
        StartCoroutine(DelayRayCheck());
    }
    IEnumerator WaterGenarating(GameObject waterReference, Vector3 waterPosition)
    {
        yield return new WaitForSeconds(0.5f);
        GameObject waterSpawn = Instantiate(waterReference, waterPosition, Quaternion.identity);
        waterSpawn.GetComponent<WaterLogic>().waterSerial = waterSerial;
    }

    void Raycheck()
    {
        
        if (!checkallow)
        {
            return;
        }

        int count = 0;

        // if (Delay <= 0)
        //{
        
        if (Physics.Raycast(gameObject.transform.position, -transform.up, out RaycastHit downHit, 3f, layertoblock))
        {
            
            if (Physics.Raycast(gameObject.transform.position, transform.forward, out RaycastHit forwardHit, 3f, layertoblock))
            {
                hitOBJ = forwardHit.transform.gameObject;
                blockState = BlockState.Forward;
                count += 1;
                OBJcheck();
            }
            else
            {
                flowDirection = direction.Forward;
                if (Delay < 0)
                {
                    StartCoroutine(WaterGenarating(waterPrefab, transform.position + new Vector3(0, 0, 5)));

                }
            }

            if (Physics.Raycast(gameObject.transform.position, -transform.forward, out RaycastHit backwardHit, 3f, layertoblock))
            {
                hitOBJ = backwardHit.transform.gameObject;
                blockState = BlockState.Backward;
                count += 1;
                OBJcheck();
            }
            else
            {
                flowDirection = direction.Backward;
                if (Delay < 0)
                {
                    StartCoroutine(WaterGenarating(waterPrefab, transform.position + new Vector3(0, 0, -5)));

                }
            }
            if (Physics.Raycast(gameObject.transform.position, transform.right, out RaycastHit rightHit, 3f, layertoblock))
            {
                hitOBJ = rightHit.transform.gameObject;
                blockState = BlockState.Right;
                count += 1;
                OBJcheck();
            }
            else
            {
                flowDirection = direction.Right;
                if (Delay < 0)
                {
                    StartCoroutine(WaterGenarating(waterPrefab, transform.position + new Vector3(5, 0, 0)));

                }

              
            }
            
            if (Physics.Raycast(gameObject.transform.position, -transform.right, out RaycastHit leftHit, 3f, layertoblock))
            {
                
                hitOBJ = leftHit.transform.gameObject;
                blockState = BlockState.Left;
                count += 1;
                OBJcheck();
            }
            else
            {
                flowDirection = direction.Left;
                if (Delay < 0)
                {
                    StartCoroutine(WaterGenarating(waterPrefab, transform.position + new Vector3(-5, 0, 0)));

                }
            }
        }
        else
        {
            StartCoroutine(WaterGenarating(waterPrefab, transform.position + new Vector3(0, -3f, 0)));
            flowDirection = direction.None;
            
        }
            //}
            //else
            //{
            //  //  Delay -= Time.deltaTime;
            //}

            if (count == 4)
            {
                blockState = BlockState.All;
            }
        }

        void OBJcheck()
        {
            if (hitOBJ != null)
            {
                if (hitOBJ.tag == "Water")
                {
                    if (hitOBJ.GetComponent<WaterLogic>())
                    {
                        WaterLogic watertocompare = hitOBJ.GetComponent<WaterLogic>();
                        
                        if (waterSerial != watertocompare.waterSerial)
                        {
                            if (otherWater != watertocompare)
                            {
                              
                               if(othercheck == true) { othercheck = false; return; }
                                int result = answerManager.waterArr[waterSerial] + answerManager.waterArr[watertocompare.waterSerial];
                                answerManager.waterArr[waterSerial] = waterValue = result;
                                answerManager.waterArr[watertocompare.waterSerial] = watertocompare.waterValue = result;
                                otherWater = watertocompare;
                                otherWater.othercheck = true;
                                answerManager.addWaterList(waterSerial, otherWater.waterSerial, result);
                               
                            }
                        }
                        else
                        {
                            if (watertocompare.waterValue < waterValue)
                            {
                                watertocompare.waterValue = waterValue;
                            }
                        }
                    }
                }
            }
            else
            {
                blockState = BlockState.None;
            }
        }
    }

