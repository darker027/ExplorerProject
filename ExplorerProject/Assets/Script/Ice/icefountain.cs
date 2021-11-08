using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class icefountain : MonoBehaviour
{
    [SerializeField] private Material Water;
    [SerializeField] private Material Ice;
    [SerializeField] private Collider boxCollider;
    [SerializeField] private float Timer = 5f;
    private bool isFreeze;
    // Start is called before the first frame update
    void Start()
    {
        isFreeze = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFreeze == true)
        {
            if (Timer > 0)
            {
                Timer -= Time.deltaTime;
            }
            else
            {
                isFreeze = false;
                boxCollider.enabled = !boxCollider.enabled;
                gameObject.GetComponent<Renderer>().material = Water;
            }
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ice") )
        {
            Timer = 5.0f;
            isFreeze = true;
            boxCollider.enabled = !boxCollider.enabled;
            gameObject.GetComponent<Renderer>().material = Ice;
        }
      
    }
}
