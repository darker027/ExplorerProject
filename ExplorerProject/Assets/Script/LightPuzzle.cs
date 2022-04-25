using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPuzzle : MonoBehaviour
{
    [SerializeField] private Transform[] checkPoints;
    [SerializeField] private int[] requireValue;

    private bool complete;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < checkPoints.Length; i++)
        {
            if (Physics.Raycast(checkPoints[i].position, Vector3.down, out RaycastHit hit))
            {
                if (hit.transform.TryGetComponent<WaterLogic>(out WaterLogic waterCheck))
                {
                    checkPoints[i].GetComponentInChildren<TMPro.TextMeshProUGUI>().enabled = true;
                    checkPoints[i].GetComponentInChildren<TMPro.TextMeshProUGUI>().text = waterCheck.waterValue.ToString();

                    if (waterCheck.waterValue >= requireValue[i])
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

        if (!complete)
        {
            this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            this.gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
    }
}