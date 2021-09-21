using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clue : MonoBehaviour
{
    [SerializeField] private Material clueColorDefault;
    [SerializeField] private Material[] clueColorMaterial;
    [SerializeField] private GameObject[] clueBox;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            for(int index = 0; index < clueBox.Length; index++)
            {
                clueBox[index].GetComponent<Renderer>().material = clueColorMaterial[index];
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            for (int index = 0; index < clueBox.Length; index++)
            {
                clueBox[index].GetComponent<Renderer>().material = clueColorDefault;
            }
        }
    }
}
