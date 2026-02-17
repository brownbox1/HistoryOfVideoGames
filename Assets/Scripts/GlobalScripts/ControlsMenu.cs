using UnityEngine;
using UnityEngine.SceneManagement;


public class ControlsMenu : MonoBehaviour
{
    public GameObject controlsPanel;
    void Start()
    {
        ShowControls();
    }

    public void ShowControls()
    {
        controlsPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void HideControls()
    {
        controlsPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
