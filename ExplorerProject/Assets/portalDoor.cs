using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class portalDoor : MonoBehaviour
{
    int nextSceneIndex;
    [SerializeField] private Animator transitionAnim;
    // Start is called before the first frame update
    void Start()
    {
         nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(LoadScene());
        }
    }
    IEnumerator LoadScene()
    {
        transitionAnim.SetTrigger("endScene");
        yield return new WaitForSeconds(1.5f);
        if (SceneManager.sceneCountInBuildSettings > nextSceneIndex)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}
