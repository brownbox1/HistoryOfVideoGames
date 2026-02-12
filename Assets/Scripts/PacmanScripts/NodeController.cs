using UnityEngine;

public class NodeController : MonoBehaviour
{
    public bool canMoveLeft = false;
    public bool canMoveRight = false;
    public bool canMoveUp = false;
    public bool canMoveDown = false;

    public GameObject nodeLeft;
    public GameObject nodeRight;
    public GameObject nodeUp;
    public GameObject nodeDown;

    public LayerMask nodeLayer;

    public bool isWarpRightNode = false;
    public bool isWarpLeftNode = false;

    public bool isPelletNode = false; // checks if node has a pellet when game starts
    public bool hasPellet = false; // checks if node still has pellet

    public bool isGhostStartingNode = false;
    public SpriteRenderer pelletSprite;
    public GameManagerPM gameManager;

    public bool isSideNode = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (transform.childCount > 0)
        {
            hasPellet = true;
            isPelletNode = true;
            pelletSprite = GetComponentInChildren<SpriteRenderer>();
        }
        RaycastHit2D[] hitsDown;
        hitsDown = Physics2D.RaycastAll(transform.position, -Vector2.up, 1f, nodeLayer);

        for(int i = 0; i < hitsDown.Length; i++)
        {
            float dist = Mathf.Abs(hitsDown[i].point.y - transform.position.y);
            if (dist < 0.4f)
            {
                canMoveDown = true;
                nodeDown = hitsDown[i].collider.gameObject;
            }
        }
        RaycastHit2D[] hitsUp;
        hitsUp = Physics2D.RaycastAll(transform.position, Vector2.up, 1f, nodeLayer);

        for(int i = 0; i < hitsUp.Length; i++)
        {
            float dist = Mathf.Abs(hitsUp[i].point.y - transform.position.y);
            if (dist < 0.4f)
            {
                canMoveUp = true;
                nodeUp = hitsUp[i].collider.gameObject;
            }
        }

        RaycastHit2D[] hitsRight;
        hitsRight = Physics2D.RaycastAll(transform.position, Vector2.right, 1f, nodeLayer);

        for(int i = 0; i < hitsRight.Length; i++)
        {
            float dist = Mathf.Abs(hitsRight[i].point.x - transform.position.x);
            if (dist < 0.4f)
            {
                canMoveRight = true;
                nodeRight = hitsRight[i].collider.gameObject;
            }
        }

        RaycastHit2D[] hitsLeft;
        hitsLeft = Physics2D.RaycastAll(transform.position, -Vector2.right, 1f, nodeLayer);

        for(int i = 0; i < hitsLeft.Length; i++)
        {
            float dist = Mathf.Abs(hitsLeft[i].point.x - transform.position.x);
            if (dist < 0.4f)
            {
                canMoveLeft = true;
                nodeLeft = hitsLeft[i].collider.gameObject;
            }
        }

        if (isGhostStartingNode)
        {
            canMoveDown = true;
            nodeDown = gameManager.ghostNodeCenter;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetNodeFromDirection(string direction)
    {
        if (direction == "left" && canMoveLeft)
        {
            return nodeLeft;
        }
        else if (direction == "right" && canMoveRight)
        {
            return nodeRight;
        }
        else if (direction == "up" && canMoveUp)
        {
            return nodeUp;
        }
        else if (direction == "down" && canMoveDown)
        {
            return nodeDown;
        }
        else
        {
            return null;
        }
    }

    void OnDrawGizmos()
{
    Gizmos.color = Color.blue;
    if (nodeUp != null) Gizmos.DrawLine(transform.position, nodeUp.transform.position);
    if (nodeDown != null) Gizmos.DrawLine(transform.position, nodeDown.transform.position);
    if (nodeLeft != null) Gizmos.DrawLine(transform.position, nodeLeft.transform.position);
    if (nodeRight != null) Gizmos.DrawLine(transform.position, nodeRight.transform.position);
}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && isPelletNode)
        {
            hasPellet = false;
            pelletSprite.enabled = false;
        }
    }
}