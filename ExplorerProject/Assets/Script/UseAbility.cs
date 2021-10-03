using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseAbility : MonoBehaviour
{
      //bullet
    public Element.currentElement currElement;
    public GameObject ability;

    //bullet force
    [SerializeField] private float shootForce,upwardForce;
    
    //element stat
    [SerializeField] private float timeBetweenShots, reloadTime;

    [SerializeField] private int bulletPerTap,magazineSize;

    int bulletLeft, bulletShot;
    //bool
    private bool allowButtonHold,allowInvoke;

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
    }

    // Update is called once per frame
    void Update()
    {
        switch (currElement)
        {
            case Element.currentElement.Fire:
                {
                    
                    break;
                }
            case Element.currentElement.Ice:
                {
                    break;
                }
            case Element.currentElement.Wind:
                {
                    break;
                }



        }
        myInput();
    }
    private void myInput()
    {
        //check if allowed to hold down button
        if (allowButtonHold)
        {
            shooting = Input.GetKey(KeyCode.Mouse0);
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

        GameObject currentBullet = Instantiate(ability, attackPoint.position, Quaternion.identity);


        //rotate bullet to shoot direction
        currentBullet.transform.forward = directionWithoutSpread.normalized;

        //addforce to bullet
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithoutSpread.normalized * shootForce, ForceMode.Impulse);

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
