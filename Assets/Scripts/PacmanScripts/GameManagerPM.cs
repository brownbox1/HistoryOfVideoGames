using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;

public class GameManagerPM : MonoBehaviour
{
    public GameObject pacman;
    public GameObject leftWarpNode;
    public GameObject rightWarpNode;
    public GameObject ghostNodeLeft;
    public GameObject ghostNodeRight;
    public GameObject ghostNodeCenter;
    public GameObject ghostNodeStart;
    

    public GameObject redGhost;
    public GameObject pinkGhost;
    public GameObject blueGhost;
    public GameObject orangeGhost;

    public int totalPellets;
    public int pelletsLeft;
    public int pelletsCollectedThisLife;

    public int score;
    public int lives = 3;
    public TextMeshProUGUI scoreText; 
    public TextMeshProUGUI livesText;
    public string menuSceneName = "MainMenu";

    public bool hadDeathOnThisLevel;
    public float frightenedTimer = 8f;

    public enum GhostMode
    {
        chase, scatter
    }

    public GhostMode currentGhostMode;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        pinkGhost.GetComponent<EnemyControllerPC>().readyToLeaveHome = true;
        currentGhostMode = GhostMode.chase;
        ghostNodeStart.GetComponent<NodeController>().isGhostStartingNode = true;
        pacman = GameObject.Find("Player");
        
    }

    void Start()
    {
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        var keyboard = Keyboard.current;
        if (keyboard.escapeKey.isPressed)
        {
            ReturnToMenu();
        }
    }

    public void GotPelletFromNodeController()
    {
        totalPellets++;
        pelletsLeft++;
    }

    public void CollectedPellet(NodeController nodeController)
    {
        pelletsLeft--;
        pelletsCollectedThisLife++;
        score += 10;
        UpdateUI();
        
        if (pelletsLeft <= 7)
        {
            FindFirstObjectByType<WinScreenManager>().HandleWin();
        }

        int requiredBluePellets = 0;
        int requiredOrangePellets = 0;

        if (hadDeathOnThisLevel)
        {
            requiredBluePellets = 12;
            requiredOrangePellets = 12;
        }
        else
        {
            requiredBluePellets = 30;
            requiredOrangePellets = 60;
        }

        if (pelletsCollectedThisLife >= requiredBluePellets && !blueGhost.GetComponent<EnemyControllerPC>().leftHomeBefore)
        {
            blueGhost.GetComponent<EnemyControllerPC>().readyToLeaveHome = true;
        }
        if (pelletsCollectedThisLife >= requiredOrangePellets && !orangeGhost.GetComponent<EnemyControllerPC>().leftHomeBefore)
        {
            orangeGhost.GetComponent<EnemyControllerPC>().readyToLeaveHome = true;
        }
        
    }

    public void pacmanDied()
    {
        lives--;
        Debug.Log("Collision between Ghost/Pacman Detected!");
        UpdateUI();

        if (lives <= 0)
        {
            ReturnToMenu();
        }
        else
        {
            pelletsCollectedThisLife = 0;
            hadDeathOnThisLevel = true;

            pacman.GetComponent<PlayerControllerPM>().Setup();
            redGhost.GetComponent<EnemyControllerPC>().Setup();
            pinkGhost.GetComponent<EnemyControllerPC>().Setup();
            blueGhost.GetComponent<EnemyControllerPC>().Setup();
            orangeGhost.GetComponent<EnemyControllerPC>().Setup();

            pinkGhost.GetComponent<EnemyControllerPC>().readyToLeaveHome = true;
        }
    }

    public void CollectedPowerPellet()
    {
        score += 50;
        UpdateUI();
        StartCoroutine(FrightenedModeRoutine());
    }

    System.Collections.IEnumerator FrightenedModeRoutine()
    {
        SetGhostsFrightened(true);

        yield return new WaitForSeconds(frightenedTimer);

        SetGhostsFrightened(false);
    }

    void SetGhostsFrightened(bool frightened)
    {
        redGhost.GetComponent<EnemyControllerPC>().isFrightened = frightened;
        pinkGhost.GetComponent<EnemyControllerPC>().isFrightened = frightened;
        blueGhost.GetComponent<EnemyControllerPC>().isFrightened = frightened;
        orangeGhost.GetComponent<EnemyControllerPC>().isFrightened = frightened;
    }

    public void UpdateUI()
        {
            if (scoreText != null)
            {
                scoreText.text = "Score: " + score;
            }
            if (livesText != null)
            {
                livesText.text = "Lives: " + lives;
            }
            if (pelletsLeft <= 3)
            {
                FindFirstObjectByType<WinScreenManager>().HandleWin();
            }
        }

    void ReturnToMenu()
    {
        SceneManager.LoadScene(menuSceneName);
    }
    
}