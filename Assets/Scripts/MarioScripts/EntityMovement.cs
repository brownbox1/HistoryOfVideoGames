using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public Vector2 direction = Vector2.left;

    public LayerMask wallLayer;
    public float wallCheckDistance = 0.6f;

    public float ledgeCheckDistance = 1f;
    public float ledgeCheckOffset = 0.5f;

    public Sprite squashSprite;
    private bool isSquashed;

    private Rigidbody2D rigidbody;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (isSquashed)
        {
            return; // stop movement when dead
        }

        rigidbody.linearVelocity = new Vector2(direction.x * moveSpeed, rigidbody.linearVelocity.y);

        CheckForWalls();
        CheckForLedges();
    }

    void CheckForWalls()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, wallCheckDistance, wallLayer);

        if (hit.collider != null)
        {
            direction = -direction;
        }
    }

    void CheckForLedges()
    {
        Vector2 checkPos = (Vector2)transform.position + (direction * ledgeCheckOffset);

        RaycastHit2D hit = Physics2D.Raycast(checkPos, Vector2.down, ledgeCheckDistance, wallLayer);

        if (hit.collider == null)
        {
            direction = -direction;
        }
    }

    public void Squash()
    {
        isSquashed = true;
        moveSpeed = 0;
        GetComponent<SpriteRenderer>().sprite = squashSprite;

        GetComponent<Collider2D>().enabled = false;

        Destroy(gameObject, 0.5f);
    }
}
