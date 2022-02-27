using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iceCube : MonoBehaviour
{
    [SerializeField] private float meltTime;
    private bool isMelt;

    [SerializeField] private GameObject[] waters;
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
            transform.localScale -= new Vector3(1, 0.5f, 1) * Time.deltaTime;
        }
        if(meltTime <= 0.0f)
        {
            for(int index = 0; index < waters.Length; index++)
            {
                waters[index].gameObject.SetActive(true);
            }
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
