using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseAbility : MonoBehaviour
{
      //bullet
    public Element.currentElement currElement;
    private GameObject ability;
    public GameObject fire;
    public GameObject ice;
    public GameObject electric;
    

    //bullet force
    [SerializeField] private float shootForce,upwardForce;
    [SerializeField] private PickableController pickableController;
    
    //element stat
    [SerializeField] private float timeBetweenShots, reloadTime;

    [SerializeField] private int bulletPerTap,magazineSize;

    [SerializeField]private float  bulletMaximumSize;
    private float bulletSize;

    int bulletLeft, bulletShot;
    //bool
    private bool allowButtonHold,allowInvoke,isCharging;

    private bool shooting, readyToShoot,reloading;


    //Ref
    [SerializeField] private Camera ThirdPersonCam;
    [SerializeField] private Transform attackPoint;

    // Start is called before the first frame update
    private void Awake()
    {
        //make sure bullet is full
        bulletLeft = magazineSize;
        readyToShoot = true;
        allowButtonHold = false;
        bulletSize = 0.0f;
    
    }

    // Update is called once per frame
    void Update()
    {
        switch (currElement)
        {
            case Element.currentElement.Fire:
                {
                    ability = fire;
                    allowButtonHold = false;
                    break;
                }
            case Element.currentElement.Ice:
                {
                    ability = ice;
                    allowButtonHold = false;
                    break;
                }
            case Element.currentElement.Electro:
                {
                    ability = electric;
                    allowButtonHold = true;

                    break;
                }



        }
        myInput();
    }
    private void myInput()
    {
        //pick up
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (pickableController.IsHolding == false)
            {
                if (pickableController.Pickup())
                {
                    pickableController.Pickup();
                }
            }
            else
            {
                if (pickableController.PutDown())
                {
                    pickableController.PutDown();
                }
            }

        }
        //check if allowed to hold down button
        if (allowButtonHold)
        {
           
            if (Input.GetKey(KeyCode.Mouse0))
            {
                if(bulletSize <= bulletMaximumSize)
                {

                    bulletSize += 5 * Time.deltaTime;
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                shooting = true;

            }
            
        }
        else
        {
            shooting = Input.GetKeyDown(KeyCode.Mouse0);
        }

        //reloading
        if(Input.GetKeyDown(KeyCode.R) && bulletLeft < magazineSize && !reloading)
        {
            Reload();
        }

        //reload auto
        if(readyToShoot && shooting && !reloading && bulletLeft <= 0)
        {
            Reload();
        }

        if(readyToShoot && shooting && !reloading && bulletLeft > 0)
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && currElement != Element.currentElement.Fire)
        {
            currElement = Element.currentElement.Fire;
        }
        if(Input.GetKeyDown(KeyCode.Alpha2) && currElement != Element.currentElement.Ice)
        {
            currElement = Element.currentElement.Ice;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && currElement != Element.currentElement.Electro)
        {
            currElement = Element.currentElement.Electro;
        }
    }
    private void Shoot()
    {
        readyToShoot = false;
        allowInvoke = true;

        //raycast
        Ray ray = ThirdPersonCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        //check if ray cast hit something

        Vector3 targetPoint;
        if(Physics.Raycast(ray,out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(75);
        }

        //calculate direction from attackpoint to targetPoint
        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

        


        if (currElement != Element.currentElement.Electro)
        {
            GameObject currentBullet = Instantiate(ability, attackPoint.position, Quaternion.identity);
            //rotate bullet to shoot direction
            currentBullet.transform.forward = directionWithoutSpread.normalized;

            //addforce to bullet
            currentBullet.GetComponent<Rigidbody>().AddForce(directionWithoutSpread.normalized * shootForce, ForceMode.Impulse);
        }
        else
        {
           
            GameObject currentBullet = Instantiate(ability, attackPoint.position, Quaternion.identity);
            currentBullet.transform.localScale = new Vector3(currentBullet.transform.localScale.x * bulletSize, currentBullet.transform.localScale.y * bulletSize, currentBullet.transform.localScale.z * bulletSize);
            currentBullet.GetComponent<ElectricAbility>().electric = bulletSize;
            
            //rotate bullet to shoot direction
            currentBullet.transform.forward = directionWithoutSpread.normalized;

            //addforce to bullet
            currentBullet.GetComponent<Rigidbody>().AddForce(directionWithoutSpread.normalized * shootForce, ForceMode.Impulse);
            bulletSize = 0.0f;
            shooting = false;
        }

        bulletLeft--;
        bulletShot++;

        if (allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShots);
            allowInvoke = false;
        }
        if(bulletShot < bulletPerTap && bulletLeft > 0)
        {
            Invoke("Shoot", timeBetweenShots);
        }
    }
    private void ResetShot()
    {
        readyToShoot = true;
        allowInvoke = true;
    }
    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }
    private void ReloadFinished()
    {
        bulletLeft = magazineSize;
        reloading = false;
    }

}
