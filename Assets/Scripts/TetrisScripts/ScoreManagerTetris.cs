using UnityEngine;
using TMPro;

public class ScoreManagerTetris : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int playerScore = 0;
    private int AIScore = 0;

    public void AddPoint(int playerNumber)
    {
        if (playerNumber == 0)
        {
            playerScore += 1;
        }
        else
        {
            AIScore += 1;
        }
        UpdateScoreDisplay();

        if (playerScore >= 5)
        {
            Debug.Log("Winner!");
        }
        if (AIScore >= 5)
        {
            Debug.Log("AI Won!");
        }

    }
    public void UpdateScoreDisplay()
    {
        scoreText.text = playerScore + " - " + AIScore;
    }

    public void resetScores()
    {
        playerScore = 0;
        AIScore = 0;
        UpdateScoreDisplay();
    }
}
