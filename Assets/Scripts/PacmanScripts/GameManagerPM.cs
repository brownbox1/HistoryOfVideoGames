using UnityEngine;

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

    public bool hadDeathOnThisLevel;

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

    // Update is called once per frame
    void Update()
    {
        
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
}