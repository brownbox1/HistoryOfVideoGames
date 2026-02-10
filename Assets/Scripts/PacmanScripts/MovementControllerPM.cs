using UnityEngine;

public class MovementControllerPM : MonoBehaviour
{
    public GameManagerPM gameManager;

    public GameObject currentNode;
    public float speed = 4f;

    public string direction = "";
    public string lastMovingDirection = "";

    public bool canWarp = true;

    public bool isGhost = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        lastMovingDirection = "left";
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerPM>();
    }

    // Update is called once per frame
    void Update()
    {
        NodeController currentNodeController = currentNode.GetComponent<NodeController>();
        if (currentNodeController == null) {
        Debug.LogError(currentNode.name + " is missing the NodeController script!");
        return;
    }

        transform.position = Vector2.MoveTowards(transform.position, currentNode.transform.position, speed * Time.deltaTime);

        // check if at center of node
        if (transform.position.x == currentNode.transform.position.x && transform.position.y == currentNode.transform.position.y)
        {
            if (isGhost)
            {
                GetComponent<EnemyControllerPC>().ReachedCenterOfNode(currentNodeController);
            }

            // if at the center of left warp, warp to the right
            if (currentNodeController.isWarpLeftNode && canWarp)
            {
                currentNode = gameManager.rightWarpNode;
                direction = "left";
                lastMovingDirection = "left";
                transform.position = currentNode.transform.position;
                canWarp = false;
            }
            else if (currentNodeController.isWarpRightNode && canWarp) // same thing for right
            {
                currentNode = gameManager.leftWarpNode;
                direction = "right";
                lastMovingDirection = "right";
                transform.position = currentNode.transform.position;
                canWarp = false;
            }
            else
            {
                GameObject newNode = currentNodeController.GetNodeFromDirection(direction);

                if (newNode != null && newNode.CompareTag("Node"))
                {
                    currentNode = newNode;
                    lastMovingDirection = direction;
                }
                else
                {
                    direction = lastMovingDirection;
                    newNode = currentNodeController.GetNodeFromDirection(direction);
                    if (newNode != null && newNode.CompareTag("Node"))
                    {
                        currentNode = newNode;
                    }
                }
            }

            
        }
        else
        {
            canWarp = true;
        }
    }

    public void SetDirection(string newDirection)
    {
        direction = newDirection;
    }
}
