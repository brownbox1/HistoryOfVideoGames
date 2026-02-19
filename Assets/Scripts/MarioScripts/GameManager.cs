using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set; }

    public int world = 1;
    public int stage = 1;
    public int lives = 3;
    public int score = 0;
    public TextMeshProUGUI scoreText;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
        {
            Destroy(gameObject);
        }
        // Keeps the score/lives when changing scenes
    }
    
    void Start()
    {
        UpdateScoreDisplay();
    }

    private void NewGame()
    {
        ResetScoresAndLives();
        LoadLevel(1, 1);
    }

    private void LoadLevel(int world, int stage)
    {
        this.world = world;
        this.stage = stage;

        SceneManager.LoadScene("Mario");
    }

    public void ResetLevel()
    {
        lives--;
        UpdateScoreDisplay();

        if (lives > 0)
        {
            LoadLevel(world, stage);
        }
        else
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("GAME OVER");
        NewGame();
    }

    public void addScore(int amount)
    {
        score += amount;
        UpdateScoreDisplay();
    }
    public void UpdateScoreDisplay()
    {
        scoreText.text = "Score: \n" + score.ToString("D6") + "\nLives: " + lives;
    }

    public void ResetScoresAndLives()
    {
        lives = 3;
        score = 0;
        UpdateScoreDisplay();
    }

}
