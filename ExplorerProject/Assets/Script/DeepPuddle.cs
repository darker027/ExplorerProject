using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeepPuddle : MonoBehaviour
{
    [SerializeField] private Material Freeze;
    [SerializeField] private Material UnFreeze;
    [SerializeField] private float freezeTime;
    [SerializeField] private BoxCollider deepCollider;
    private string scenename;
    private bool IsFreeze;
    void Start()
    {
        gameObject.GetComponent<Renderer>().material = UnFreeze;
        IsFreeze = false;
        scenename = "Prototype_01";
    }

    // Update is called once per frame
    void Update()
    {
        if (IsFreeze)
        {
            freezeTime -= Time.deltaTime;
            
            gameObject.GetComponent<Renderer>().material = Freeze;

            if (freezeTime <= 0)
            {
                deepCollider.enabled = !deepCollider.enabled;
                IsFreeze = false;
            }
        }
        else
        {
            gameObject.GetComponent<Renderer>().material = UnFreeze;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && IsFreeze == false)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (other.CompareTag("Ice"))
        {
            freezeTime = 5;
            deepCollider.enabled = !deepCollider.enabled;
            IsFreeze = true;
        }
    }
}
