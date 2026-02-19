using UnityEngine;
using System.Collections;

public class MushroomBehavior : MonoBehaviour
{
    public float moveSpeed = 3f;
    private Vector2 moveDirection = Vector2.right;
    public LayerMask wallLayer;

    private Rigidbody2D rigidbody;
    private Collider2D collider;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        
        StartCoroutine(Emerge());
    }

    IEnumerator Emerge()
    {
        // disable physics and collision
        rigidbody.isKinematic = true;
        collider.enabled = false;

        // move mushroom slowly up
        Vector3 startPos = transform.position;
        Vector3 endPos = transform.position + Vector3.up;
        float elapsed = 0f;
        float duration = 0.5f;

        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(startPos, endPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = endPos;
        
        // Re-enable physics and collision
        rigidbody.isKinematic = false;
        collider.enabled = true;
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
