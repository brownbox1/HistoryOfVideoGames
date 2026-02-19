using UnityEngine;

public class MushroomBehavior : MonoBehaviour
{
    public float moveSpeed = 3f;
    private Vector2 moveDirection = Vector2.right;
    public LayerMask wallLayer;

    private Rigidbody2D rigidbody;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // move horizontally and maintain current gravity/falling speed
        rigidbody.linearVelocity = new Vector2(moveDirection.x * moveSpeed, rigidbody.linearVelocity.y);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDirection, 0.6f, wallLayer);
        if (hit.collider != null)
        {
            moveDirection = -moveDirection;
        }
    }
}
