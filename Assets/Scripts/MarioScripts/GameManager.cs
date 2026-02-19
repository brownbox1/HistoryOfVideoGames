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

    public float timeRemaining = 400f;
    private bool timerIsRunning = false;
    
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
        StartTimer();
    }
    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateScoreDisplay();
            }
            else
            {
                // time ran out
                timeRemaining = 0;
                timerIsRunning = false;
                ResetLevel();
            }
        }
    }

    public void StartTimer()
    {
        timerIsRunning = true;
    }
    public void StopTimer()
    {
        timerIsRunning = false;
    }

    public void TimeBonus()
    {
        StopTimer();

        // turn remaining seconds into score
        int timeBonus = Mathf.FloorToInt(timeRemaining) * 50;
        addScore(timeBonus);
        
        Debug.Log("Score added with time Bonus: " + timeBonus);
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

        timeRemaining = 400f;
        StartTimer();

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
        scoreText.text = "    MARIO                              LIVES             TIME\n" +
                         "    " + score.ToString("D6") + "                               " + lives + "                " + Mathf.FloorToInt(timeRemaining);
    }

    public void ResetScoresAndLives()
    {
        lives = 3;
        score = 0;
        UpdateScoreDisplay();
    }

}
