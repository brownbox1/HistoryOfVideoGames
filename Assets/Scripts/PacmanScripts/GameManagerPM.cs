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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        ghostNodeStart.GetComponent<NodeController>().isGhostStartingNode = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}