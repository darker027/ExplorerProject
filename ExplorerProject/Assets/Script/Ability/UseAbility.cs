using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UseAbility : MonoBehaviour
{
      //bullet
    public Element.currentElement currElement;
    private GameObject ability;
    public GameObject fire;
    public GameObject ice;
    //public GameObject electric;


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

    private bool isClickSwitch;

    //for checking upgrade
    //private bool haveTorch;
    [SerializeField] private bool haveIce;
    [SerializeField] private GameObject childLight;

    //Ref
    [SerializeField] private Camera ThirdPersonCam;
    [SerializeField] private Transform attackPoint;
    private Transform spawnPoint;

    //singleton
    private static UseAbility _instance; //{ get; private set; }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    // Start is called before the first frame update
    private void Awake()
    {
        //make sure bullet is full
        bulletLeft = magazineSize;
        readyToShoot = true;
        allowButtonHold = false;
        bulletSize = 0.0f;
       // haveIce = false;
        isClickSwitch = false;
        //haveTorch = false;
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);

    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
       if(ThirdPersonCam == null)
       {
            ThirdPersonCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>() as Camera;
       }
        spawnPoint = GameObject.FindWithTag("Respawn").transform;
        this.transform.position = spawnPoint.position;
      

    }

    public static UseAbility Instance
    {
        get
        {

            if (_instance == null)
            {
                _instance = FindObjectOfType<UseAbility>();
                
            }

            return _instance;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "EndScene")
        {
            Destroy(this.gameObject);
        }
        //reload scene
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (isClickSwitch == true)
        {
            return;
        }
        

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
            /*case Element.currentElement.Electro:
                {
                    ability = electric;
                    allowButtonHold = true;

                    break;
                }*/



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
                
              pickableController.Pickup();
                
            }
            else
            {
               
                    pickableController.PutDown();
             
            }

        }
        if (Input.GetKeyDown(KeyCode.Alpha1) && currElement != Element.currentElement.Fire)
        {
            currElement = Element.currentElement.Fire;
            Debug.Log("Fire");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && currElement != Element.currentElement.Ice && haveIce == true)
        {
            Debug.Log("Ice");
            currElement = Element.currentElement.Ice;
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

      
      /*  if (Input.GetKeyDown(KeyCode.Alpha3) && currElement != Element.currentElement.Electro)
        {
            currElement = Element.currentElement.Electro;
        }*/
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
    // implement later after add animation
    public void isDead()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == ("upIce"))
        {
            Destroy(other.gameObject);
            haveIce = true;
        }
        if (other.tag == ("upLight"))
        {
            Destroy(other.gameObject);
            //haveTorch = true;
          
            childLight.SetActive(true);
            
        }
    }
    public void setSwitchAbility(bool x)
    {
        isClickSwitch = x;
    }
}
