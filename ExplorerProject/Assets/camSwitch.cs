using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camSwitch : MonoBehaviour
{
    [SerializeField] private GameObject topViewCam;
    [SerializeField] private moveSpotLight SpotLight;
    private GameObject player;
    private bool playerNear;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerNear = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerNear == false)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            player.GetComponent<UseAbility>().setSwitchAbility(true);
            player.GetComponent<PlayerMovement>().setSwitchMovement(true);
            SpotLight.setSwitchSpotlight(true);
            topViewCam.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            player.GetComponent<UseAbility>().setSwitchAbility(false);
            player.GetComponent<PlayerMovement>().setSwitchMovement(false);
            SpotLight.setSwitchSpotlight(false);
            topViewCam.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        playerNear = false;
    }
}
