using UnityEngine;
using System.Collections.Generic;

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

    public EnemyControllerPC redGhostController;
    public EnemyControllerPC pinkGhostController;
    public EnemyControllerPC blueGhostController;
    public EnemyControllerPC orangeGhostController;

    public int totalPellets;
    public int pelletsLeft;
    public int pelletsCollectedThisLife;

    public bool hadDeathOnThisLevel;

    public bool gameIsRunning;

    public List<NodeController> nodeControllers = new List<NodeController>();

    public bool newGame;
    public bool clearedLevel;

    public enum GhostMode
    {
        chase, scatter
    }

    public GhostMode currentGhostMode;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        newGame= true;
        clearedLevel = false;

        redGhostController = redGhost.GetComponent<EnemyControllerPC>();
        pinkGhostController = pinkGhost.GetComponent<EnemyControllerPC>();
        blueGhostController = blueGhost.GetComponent<EnemyControllerPC>();
        orangeGhostController = orangeGhost.GetComponent<EnemyControllerPC>();

        gameIsRunning = true;
        pinkGhost.GetComponent<EnemyControllerPC>().readyToLeaveHome = true;
        currentGhostMode = GhostMode.chase;
        ghostNodeStart.GetComponent<NodeController>().isGhostStartingNode = true;
        pacman = GameObject.Find("Player");
    }

    public void Setup()
    {
        for (int i = 0; i < nodeControllers.Count; i++)
        {
            nodeControllers[i].RespawnPellet();
        }
        pacman.GetComponent<PlayerControllerPM>().Setup();
        
        redGhostController.Setup();
        pinkGhostController.Setup();
        blueGhostController.Setup();
        orangeGhostController.Setup();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GotPelletFromNodeController(NodeController nodecontroller)
    {
        nodeControllers.Add(nodecontroller);
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