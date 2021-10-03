using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Initializiation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
    protected virtual void Initializiation()
    {
    }



}
