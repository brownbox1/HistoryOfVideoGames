using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.InputSystem;
using TMPro;

public class WinScreenManager : MonoBehaviour
{

    public GameObject winCanvas;
    public GameObject blurVolume;
    public float delayBeforeMenu = 3f;
    public string mainMenuName = "MainMenu";

    public void HandleWin()
    {
        StartCoroutine(WInSequence());
    }

    IEnumerator WInSequence()
    {
        winCanvas.SetActive(true);
        if (blurVolume != null)
        {
            blurVolume.SetActive(true);
        }

        yield return new WaitForSeconds(delayBeforeMenu);

        SceneManager.LoadScene(mainMenuName);
    }

}
