using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iceCube : MonoBehaviour
{
    [SerializeField] private float meltTime;
    private bool isMelt;

    [SerializeField] [Range(0.0f, 1.0f)] private float lerpTime;
    [SerializeField] Color meltColor;

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
            transform.localScale -= new Vector3(0, 0.5f, 0) * Time.deltaTime;
            GetComponent<MeshRenderer>().material.color = Color.Lerp(GetComponent<MeshRenderer>().material.color, meltColor, lerpTime);
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
