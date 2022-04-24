using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class answerManager : MonoBehaviour
{
    [SerializeField] private GameObject Ans1;
    [SerializeField] private GameObject Ans2;
    [SerializeField] private GameObject Ans3;



    [SerializeField] private GameObject Prize;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if(Ans1 == null && Ans2 == null && Ans3 == null)
        {
            Prize.SetActive(true);
        }
    }
}
