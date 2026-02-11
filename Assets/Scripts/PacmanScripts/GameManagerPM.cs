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

    public enum GhostMode
    {
        chase, scatter
    }

    public GhostMode currentGhostMode;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        currentGhostMode = GhostMode.chase;
        ghostNodeStart.GetComponent<NodeController>().isGhostStartingNode = true;
        pacman = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}