using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iceCube : MonoBehaviour
{
    [SerializeField] private float meltTime;
    private bool isMelt;
    [SerializeField] LayerMask layertoblock;
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
            
            shootRay();
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

    void shootRay()
    {


        if (Physics.Raycast(gameObject.transform.position, transform.forward, out RaycastHit forwardHit, 3f, layertoblock))
        {
            WaterLogic waterLogic;
            if ((waterLogic = forwardHit.collider.GetComponent<WaterLogic>()) != null)
            {
                Debug.Log("na heee1");
                waterLogic.startDelayRayCheck();
                return;
            }
            
        }


        if (Physics.Raycast(gameObject.transform.position, -transform.forward, out RaycastHit backwardHit, 3f, layertoblock))
        {
            WaterLogic waterLogic;
            if ((waterLogic = backwardHit.collider.GetComponent<WaterLogic>()) != null)
            {
                Debug.Log("na heee2");
                waterLogic.startDelayRayCheck();
                return;
            }
        }


        if (Physics.Raycast(gameObject.transform.position, transform.right, out RaycastHit rightHit, 3f, layertoblock))
        {
            WaterLogic waterLogic;
            if ((waterLogic = rightHit.collider.GetComponent<WaterLogic>()) != null)
            {
                
                waterLogic.startDelayRayCheck();
                return;
            }
        }



        if (Physics.Raycast(gameObject.transform.position, -transform.right, out RaycastHit leftHit, 3f, layertoblock))
        {
            WaterLogic waterLogic;
            if ((waterLogic = leftHit.collider.GetComponent<WaterLogic>()) != null)
            {
                Debug.Log("na heee4");
                waterLogic.startDelayRayCheck();
                return;
            }
        }


    }
}


