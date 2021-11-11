using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeSaw : MonoBehaviour
{
    [SerializeField] private Material Water;
    [SerializeField] private Material Ice;

    private Rigidbody RB;

    [SerializeField] private bool onFreeze;

    [SerializeField] private float freezeTime;

    // Start is called before the first frame update
    void Start()
    {
        RB = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(onFreeze)
        {
            freezeTime -= Time.deltaTime;
            RB.freezeRotation = true;
            gameObject.GetComponent<Renderer>().material = Ice;

            if (freezeTime <= 0)
            {
                onFreeze = false;
            }
        }
        else
        {
            RB.freezeRotation = false;
            gameObject.GetComponent<Renderer>().material = Water;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ice"))
        {
            freezeTime = 5;
            onFreeze = true;
        }
    }
}
