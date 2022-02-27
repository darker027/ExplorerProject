using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iceCube : MonoBehaviour
{
    [SerializeField] private float meltTime;
    private bool isMelt;
    // Start is called before the first frame update
    void Start()
    {
        isMelt = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMelt)
        {
            meltTime -= Time.deltaTime;
        }
        if(meltTime <= 0.0f)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("sunLight"))
        {
            isMelt = true;
        }

        
    }
    private void OnTriggerExit(Collider other)
    {
        isMelt = false;
    }

}
