using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFinish : MonoBehaviour
{
    [SerializeField] private GameObject victoryPopup;
    [SerializeField] private GameObject defeatPopup;

    // Only allows the "level" to finish once.
    private bool hasLevelFinished = false;

    // Update is called once per frame
    void Update()
    {
        if (hasLevelFinished)
        {
            return;
        }

        if (GameManager.Instance.PlayerHealth <= 0)
        {
            hasLevelFinished = true;
            victoryPopup.SetActive(true);
            StartCoroutine(NextLevel());
            return;
        }

        if (GameObject.FindGameObjectsWithTag("Player").Length == 0)
        {
            hasLevelFinished = true;
            defeatPopup.SetActive(true);
            StartCoroutine(ResetScene());
            return;
        }
    }

    IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(5.0f);
        victoryPopup.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    IEnumerator ResetScene()
    {
        yield return new WaitForSeconds(5.0f);
        defeatPopup.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
