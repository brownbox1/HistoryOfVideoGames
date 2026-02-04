using UnityEngine;

public class TennisPhysics : MonoBehaviour
{
    public Vector2 velocity;
    public float gravity = -9.8f;
    public float floorY = -5f;
    public float netHeight = -3f;

    // Update is called once per frame
    void Update()
    {
        // Add gravity to vertical Velo
        velocity.y += gravity * Time.deltaTime;

        // change position based on Velo
        transform.position += (Vector3)velocity * Time.deltaTime;

        // floor bounces
        if (transform.position.y <= floorY)
        {
            transform.position = new Vector3(transform.position.x, floorY, 0);
            velocity.y = -velocity.y * 0.8f;
        }

        // Net Colission
        if (Mathf.Abs(transform.position.x) < 0.1f && transform.position.y < netHeight)
        {
            velocity.x = -velocity.x * 0.5f;
        }
    }
    public void HitBall(float angleDegrees, float power)
    {
        float rad = angleDegrees * Mathf.Deg2Rad;
        velocity = new Vector2(Mathf.Cos(rad) * power, Mathf.Sin(rad) * power);
    }
}
