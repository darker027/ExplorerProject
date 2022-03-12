using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class eventManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuScreen;
    [SerializeField] private GameObject creditScreen;
    [SerializeField] private GameObject optionScreen;
    // Start is called before the first frame update

    public void tutorialButton()
    {
        SceneManager.LoadScene("L01",LoadSceneMode.Single);
    }
    public void QuitButton()
    {
            
    }
    public void OptionButton()
    {
        mainMenuScreen.SetActive(false);
        optionScreen.SetActive(true);
    }
    public void CreditButton()
    {
        mainMenuScreen.SetActive(false);
        creditScreen.SetActive(true);
    }
    public void optionBackButton()
    {
        mainMenuScreen.SetActive(true);
        optionScreen.SetActive(false);
    }
    public void CreditBackButton()
    {
        mainMenuScreen.SetActive(true);
        creditScreen.SetActive(false);
    }
}
