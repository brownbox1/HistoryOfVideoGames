using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    void Update()
    {
        var keyboard = Keyboard.current;
        if (keyboard.escapeKey.isPressed)
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            if (currentSceneName == "MainMenu")
            {
                QuitGame();
            }
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quit Button Pressed!");
        Application.Quit();
    }
}
