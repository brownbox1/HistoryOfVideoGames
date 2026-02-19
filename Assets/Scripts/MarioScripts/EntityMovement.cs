using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public Vector2 direction = Vector2.left;
    public LayerMask wallLayer;
    public float wallCheckDistance = 0.6f;

    private Rigidbody2D rigidbody;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rigidbody.linearVelocity = new Vector2(direction.x * moveSpeed, rigidbody.linearVelocity.y);

        CheckForWalls();
    }

    void CheckForWalls()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, wallCheckDistance, wallLayer);

        if (hit.collider != null)
        {
            direction = -direction;
        }
    }
}
