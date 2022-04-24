using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [Header("Objects Reference")]
    [SerializeField] private PlayerMovement playerMovementSC;

    [SerializeField] private Slider staminaBar;
    [SerializeField] private TMPro.TextMeshProUGUI cooldownText;

    // Start is called before the first frame update
    void Start()
    {
        playerMovementSC = this.gameObject.GetComponent<PlayerMovement>();

        staminaBar.minValue = 0;
        staminaBar.maxValue = 10;
    }

    // Update is called once per frame
    void Update()
    {
        staminaBar.value = playerMovementSC.playerStamina;

        if(playerMovementSC.Exhaust)
        {
            cooldownText.enabled = true;
        }
        else
        {
            cooldownText.enabled = false;
        }

        cooldownText.text = "Exhausted : " + playerMovementSC.playerCDTime.ToString();
    }
}
