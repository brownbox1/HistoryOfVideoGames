using UnityEngine;
using System.Collections.Generic;

public class EnemyControllerPC : MonoBehaviour
{

    public enum GhostNodeStatesEnum
    {
        respawning,
        leftNode,
        rightNode,
        centerNode,
        startNode,
        movingInNodes
    }

    public GhostNodeStatesEnum ghostNodeState;
    public GhostNodeStatesEnum respawnState;

    public enum GhostType
    {
        red,
        blue,
        pink,
        orange
    }

    public GhostType ghostType;

    public GameObject ghostNodeLeft;
    public GameObject ghostNodeRight;
    public GameObject ghostNodeStart;
    public GameObject ghostNodeCenter;

    public MovementControllerPM movementController;
    public GameObject startingNode;
    
    public bool readyToLeaveHome = false;

    public GameManagerPM gameManager;

    public bool testRespawn = false;

    public bool isFrightened = false;

    public GameObject[] scatterNodes;
    public int scatterNodeIndex;

    public bool leftHomeBefore = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        scatterNodeIndex = 0;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerPM>();
        movementController = GetComponent<MovementControllerPM>();
        if (ghostType == GhostType.red)
        {
            ghostNodeState = GhostNodeStatesEnum.startNode;
            respawnState = GhostNodeStatesEnum.centerNode;
            startingNode = ghostNodeStart;
            readyToLeaveHome = true;
            leftHomeBefore = true;
        }
        else if (ghostType == GhostType.pink)
        {
            ghostNodeState = GhostNodeStatesEnum.respawning;
            respawnState = GhostNodeStatesEnum.centerNode;
            startingNode = ghostNodeCenter;
        }
        else if (ghostType == GhostType.blue)
        {
            ghostNodeState = GhostNodeStatesEnum.leftNode;
            respawnState = GhostNodeStatesEnum.leftNode;
            startingNode = ghostNodeLeft;
        }
        else if (ghostType == GhostType.orange)
        {
            ghostNodeState = GhostNodeStatesEnum.rightNode;
            respawnState = GhostNodeStatesEnum.rightNode;
            startingNode = ghostNodeRight;
        }
        movementController.currentNode = startingNode;
        transform.position = startingNode.transform.position;

    

    }

    // Update is called once per frame
    void Update()
    {
        if (testRespawn == true)
        {
            readyToLeaveHome = false;
            ghostNodeState = GhostNodeStatesEnum.respawning;
            testRespawn = false;
        }

        if (movementController.currentNode.GetComponent<NodeController>().isSideNode)
        {
            movementController.SetSpeed(1);
        }
        else
        {
            movementController.SetSpeed(3);
        }
    }

    public void ReachedCenterOfNode(NodeController nodeController)
    {
        if (ghostNodeState == GhostNodeStatesEnum.movingInNodes)
        {
            leftHomeBefore = true;
            // scatter mode
            if (gameManager.currentGhostMode == GameManagerPM.GhostMode.scatter)
            {
                DetermineGhostScatterModeDirection();
            }
            // frightened mode
            else if (isFrightened)
            {
                string direction = GetRandomDirection();
                movementController.SetDirection(direction);
            }
            // chase mode
            else
            {
                if (ghostType == GhostType.red)
                    {
                        DetermineRedGhostDirection();
                    }
                else if (ghostType == GhostType.pink)
                {
                    DeterminePinkGhostDirection();
                }
                else if (ghostType == GhostType.blue)
                {
                    DetermineBlueGhostDirection();
                }
                else if (ghostType == GhostType.orange)
                {
                    DetermineOrangeGhostDirection();
                }
            }
            
        }
        else if (ghostNodeState == GhostNodeStatesEnum.respawning)
        {
            string direction = "";
            Vector2 myPos = transform.position;
            Vector2 startPos = ghostNodeStart.transform.position;
            Vector2 centerPos = ghostNodeCenter.transform.position;
            // we reached our start node, move to the center node
            if (Vector2.Distance(myPos, startPos) < 0.1f)
            {
                direction = "down";
            }
            // we reached center node, either finish respawn or move left/right
            else if (Vector2.Distance(myPos, centerPos) < 0.01f)
            {
                if (respawnState == GhostNodeStatesEnum.centerNode)
                {
                    ghostNodeState = respawnState;
                }
                else if (respawnState == GhostNodeStatesEnum.leftNode)
                {
                    direction = "left";
                }
                else if (respawnState == GhostNodeStatesEnum.rightNode)
                {
                    direction = "right";
                }
            }
            else if ( // if our respawn state is left/right node and we got to the node, leave home
                (transform.position.x == ghostNodeLeft.transform.position.x && transform.position.y == ghostNodeLeft.transform.position.y)
                || (transform.position.x == ghostNodeRight.transform.position.x && transform.position.y == ghostNodeRight.transform.position.y))
            {
                ghostNodeState = respawnState;
            }
            else // we are in gameboard still, locate the start
            {
                direction = GetClosestDirection(ghostNodeStart.transform.position);
            }
            // find quickest direction to home
            movementController.SetDirection(direction);
        }
        else
        {
            // we are ready to leave home
            if (readyToLeaveHome)
            {
                // depending on node state, move to start
                if (ghostNodeState == GhostNodeStatesEnum.leftNode)
                {
                    ghostNodeState = GhostNodeStatesEnum.centerNode;
                    movementController.SetDirection("right");
                }
                else  if (ghostNodeState == GhostNodeStatesEnum.rightNode)
                {
                    ghostNodeState = GhostNodeStatesEnum.centerNode;
                    movementController.SetDirection("left");
                }
                else if (ghostNodeState == GhostNodeStatesEnum.centerNode)
                {
                    ghostNodeState = GhostNodeStatesEnum.startNode;
                    movementController.SetDirection("up");
                }
                else if (ghostNodeState == GhostNodeStatesEnum.startNode)
                {
                    ghostNodeState = GhostNodeStatesEnum.movingInNodes;
                    movementController.SetDirection("left");
                }

                movementController.currentNode = nodeController.GetNodeFromDirection(movementController.direction);
            }
        }
    }

    void DetermineRedGhostDirection()
    {
        string direction = GetClosestDirection(gameManager.pacman.transform.position);
        movementController.SetDirection(direction);
    }
    void DeterminePinkGhostDirection()
    {
        string pacmansDirection = gameManager.pacman.GetComponent<MovementControllerPM>().lastMovingDirection;
        float distanceBeteenNodes = 0.35f;

        Vector2 target = gameManager.pacman.transform.position;
        if (pacmansDirection == "left")
        {
            target.x -= (distanceBeteenNodes*2);
        }
        else if (pacmansDirection == "right")
        {
            target.x += (distanceBeteenNodes*2);
        }
        else if (pacmansDirection == "up")
        {
            target.y -= (distanceBeteenNodes*2);
        }
        else if (pacmansDirection == "down")
        {
            target.y += (distanceBeteenNodes*2);
        }
        string direction = GetClosestDirection(target);
        movementController.SetDirection(direction);

    }
    void DetermineBlueGhostDirection()
    {
        string pacmansDirection = gameManager.pacman.GetComponent<MovementControllerPM>().lastMovingDirection;
        float distanceBeteenNodes = 0.35f;

        Vector2 target = gameManager.pacman.transform.position;
        if (pacmansDirection == "left")
        {
            target.x -= (distanceBeteenNodes*2);
        }
        else if (pacmansDirection == "right")
        {
            target.x += (distanceBeteenNodes*2);
        }
        else if (pacmansDirection == "up")
        {
            target.y -= (distanceBeteenNodes*2);
        }
        else if (pacmansDirection == "down")
        {
            target.y += (distanceBeteenNodes*2);
        }
        GameObject redGhost = gameManager.redGhost;
        float xDist = target.x - redGhost.transform.position.x;
        float yDist = target.y - redGhost.transform.position.y;

        Vector2 blueTarget = new Vector2(target.x + xDist, target.y + yDist);
        string direction = GetClosestDirection(blueTarget);
        movementController.SetDirection(direction);

    }
    void DetermineOrangeGhostDirection()
    {
        float distance = Vector2.Distance(gameManager.pacman.transform.position, transform.position);
        float distanceBetweenNodes = 0.35f;
        if (distance < 0)
        {
            distance *= -1;
        }
        // we are within 8 nodes of pacman, use red's logic
        if (distance <= distanceBetweenNodes * 8)
        {
            DetermineRedGhostDirection();
        }
        else
        {
            // scatter mode
            DetermineGhostScatterModeDirection();
        }
    }
    string GetClosestDirection(Vector2 target)
    {
        float shortestDistance = 0;
        string lastMovingDirection = movementController.lastMovingDirection;
        string newDirection = "";
        NodeController nodeController = movementController.currentNode.GetComponent<NodeController>();
        if (nodeController.canMoveUp && lastMovingDirection != "down")
        {
            // find node above us
            GameObject nodeUp = nodeController.nodeUp;
            // find distance between pacman and top node
            float distance = Vector2.Distance(nodeUp.transform.position, target);

            // if this is the shortest distance so far, set the direction
            if (distance < shortestDistance || shortestDistance == 0)
            {
                shortestDistance = distance;
                newDirection = "up";
            }
        }

        if (nodeController.canMoveDown && lastMovingDirection != "up")
        {
            // find node above us
            GameObject nodeDown = nodeController.nodeDown;
            // find distance between pacman and top node
            float distance = Vector2.Distance(nodeDown.transform.position, target);

            // if this is the shortest distance so far, set the direction
            if (distance < shortestDistance || shortestDistance == 0)
            {
                shortestDistance = distance;
                newDirection = "down";
            }
        }

        if (nodeController.canMoveLeft && lastMovingDirection != "right")
        {
            // find node above us
            GameObject nodeLeft = nodeController.nodeLeft;
            // find distance between pacman and top node
            float distance = Vector2.Distance(nodeLeft.transform.position, target);

            // if this is the shortest distance so far, set the direction
            if (distance < shortestDistance || shortestDistance == 0)
            {
                shortestDistance = distance;
                newDirection = "left";
            }
        }

        if (nodeController.canMoveRight && lastMovingDirection != "left")
        {
            // find node above us
            GameObject nodeRight = nodeController.nodeRight;
            // find distance between pacman and top node
            float distance = Vector2.Distance(nodeRight.transform.position, target);

            // if this is the shortest distance so far, set the direction
            if (distance < shortestDistance || shortestDistance == 0)
            {
                shortestDistance = distance;
                newDirection = "right";
            }
        }
        return newDirection;
    }

    void DetermineGhostScatterModeDirection()
    {
       // if we reached scattter node, add 1 to index and move to next node
                if ((transform.position.x == scatterNodes[scatterNodeIndex].transform.position.x) && (transform.position.y == scatterNodes[scatterNodeIndex].transform.position.y))
                scatterNodeIndex++;
                if (scatterNodeIndex == scatterNodes.Length-1)
                {
                    scatterNodeIndex = 0;
                }
                string direction = GetClosestDirection(scatterNodes[scatterNodeIndex].transform.position);
                movementController.SetDirection(direction); 
    }

    string GetRandomDirection()
    {
        List<string> possibleDirections = new List<string>();
        NodeController nodeController = movementController.currentNode.GetComponent<NodeController>();

        if (nodeController.canMoveDown && movementController.lastMovingDirection != "up")
        {
            possibleDirections.Add("down");
        }
        if (nodeController.canMoveUp && movementController.lastMovingDirection != "down")
        {
            possibleDirections.Add("up");
        }
        if (nodeController.canMoveRight && movementController.lastMovingDirection != "left")
        {
            possibleDirections.Add("right");
        }
        if (nodeController.canMoveLeft && movementController.lastMovingDirection != "right")
        {
            possibleDirections.Add("left");
        }
        string direction = "";
        int randomDirectionIndex = Random.Range(0, possibleDirections.Count - 1);
        if (nodeController.isWarpRightNode)
        {
            direction = "right";
        }
        else if (nodeController.isWarpLeftNode)
        {
            direction = "left";
        }
        else
        {
            direction = possibleDirections[randomDirectionIndex];
        }
        
        return direction;
    }
}