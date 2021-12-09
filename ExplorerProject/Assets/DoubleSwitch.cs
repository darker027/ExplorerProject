using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleSwitch : MonoBehaviour
{
    [SerializeField] private GameObject WaterGroup;
    [SerializeField] private GameObject Door;
    [SerializeField] private IceSwitch switch1;
    [SerializeField] private IceSwitch switch2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(switch1.IsOn == true && switch2.IsOn == true)
        {
            Door.SetActive(false);
            WaterGroup.SetActive(true);
        }
    }
}
