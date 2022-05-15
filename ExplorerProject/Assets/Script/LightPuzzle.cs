using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPuzzle : MonoBehaviour
{
    [SerializeField] private Transform[] checkPoints;
    [SerializeField] private int[] requireValue;

    private bool complete;
    private bool Cheat;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Cheat)
        {
            complete = true;
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.P))
            {
                Cheat = true;
            }

            for (int i = 0; i < checkPoints.Length; i++)
            {
                if (Physics.Raycast(checkPoints[i].position, Vector3.down, out RaycastHit hit, float.MaxValue))
                {
                    if (hit.transform.TryGetComponent<WaterLogic>(out WaterLogic waterCheck))
                    {
                        checkPoints[i].GetComponentInChildren<TMPro.TextMeshProUGUI>().enabled = true;
                        checkPoints[i].GetComponentInChildren<TMPro.TextMeshProUGUI>().text = answerManager.waterArr[waterCheck.waterSerial].ToString();

                        if (answerManager.waterArr[waterCheck.waterSerial] >= requireValue[i])
                        {
                            complete = true;
                        }
                        else
                        {
                            complete = false;
                            break;
                        }
                    }
                    else
                    {
                        checkPoints[i].GetComponentInChildren<TMPro.TextMeshProUGUI>().enabled = false;
                        complete = false;
                        break;
                    }
                }
            }
        }

        if (!complete)
        {
            this.gameObject.GetComponent<MeshRenderer>().enabled = false;
            this.gameObject.GetComponent<SphereCollider>().enabled = false;
        }
        else
        {
            this.gameObject.GetComponent<MeshRenderer>().enabled = true;
            this.gameObject.GetComponent<SphereCollider>().enabled = true;
        }
    }
}