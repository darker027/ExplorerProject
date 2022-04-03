using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceDrop : MonoBehaviour
{
    [SerializeField] private GameObject iceBullet;

    bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Icedrop());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Icedrop()
    {
        while(!gameOver)
        {
            Instantiate(iceBullet, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(10f);
        }
    }
}
