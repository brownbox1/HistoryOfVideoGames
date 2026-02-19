using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.scoreText = this.scoreText;
            GameManager.Instance.UpdateScoreDisplay();
        }
        else
        {
            Debug.Log("No GameManager found");
        }
        
    }

}
