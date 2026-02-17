using UnityEngine;
using TMPro;

public class ScoreManagerTetris : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int linesCleared = 0;

    public void addScore()
    {
        linesCleared++;
        UpdateScoreDisplay();

        if (linesCleared >= 40)
        {
            FindFirstObjectByType<WinScreenManager>().HandleWin();
        }

    }
    public void UpdateScoreDisplay()
    {
        scoreText.text = "Score: \n" + (linesCleared * 1000);
    }

    public void resetScores()
    {
        linesCleared = 0;
        UpdateScoreDisplay();
    }
}
