using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterDetector : MonoBehaviour
{
    public int waterCount;
    private List<int> waterFoundList;
    private bool alreadyExist;

    private void Start()
    {
        alreadyExist = false;
        waterFoundList = new List<int>();
    }
    private void Update()
    {
        waterCount = waterFoundList.Count;
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.GetComponent<waterInfo>().waterId);
        alreadyExist = waterFoundList.Contains(other.gameObject.GetComponent<waterInfo>().waterId);
        if (!alreadyExist)
        {
            waterFoundList.Add(other.gameObject.GetComponent<waterInfo>().waterId);
        }
    }
}
