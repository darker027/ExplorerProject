using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterLogic : MonoBehaviour
{
    private bool flowCheck;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        //Water logic
        if(!flowCheck)
        {
            // Checking flowing downward
            if (Physics.Raycast(gameObject.transform.position, -transform.up, out RaycastHit downHit, 1.25f))
            {
                // Checking flowing forward
                if (Physics.Raycast(gameObject.transform.position, transform.forward, out RaycastHit forwardHit, 2.5f))
                {

                }
                else
                {
                    StartCoroutine(WaterGenarating(this.gameObject, transform.position + new Vector3(0, 0, 5)));
                }
                // Checking flowing backward
                if (Physics.Raycast(gameObject.transform.position, -transform.forward, out RaycastHit backwardHit, 2.5f))
                {

                }
                else
                {
                    StartCoroutine(WaterGenarating(this.gameObject, transform.position + new Vector3(0, 0, -5)));
                }
                // Checking flowing right
                if (Physics.Raycast(gameObject.transform.position, transform.right, out RaycastHit rightHit, 2.5f))
                {

                }
                else
                {
                    StartCoroutine(WaterGenarating(this.gameObject, transform.position + new Vector3(5, 0, 0)));
                }
                // Checking flowing left
                if (Physics.Raycast(gameObject.transform.position, -transform.right, out RaycastHit leftHit, 2.5f))
                {

                }
                else
                {
                    StartCoroutine(WaterGenarating(this.gameObject, transform.position + new Vector3(-5, 0, 0)));
                }

                flowCheck = true;
                return;
            }
            else
            {
                StartCoroutine(WaterGenarating(this.gameObject, transform.position + new Vector3(0, -2.5f, 0)));
                flowCheck = true;
                return;
            }
        }

    }

    IEnumerator WaterGenarating(GameObject waterReference, Vector3 waterPosition)
    {
        yield return new WaitForSeconds(1.0f);
        GameObject waterSpawn = Instantiate(waterReference, waterPosition, Quaternion.identity);
    }
}
