using UnityEngine;

public class TennisPhysics : MonoBehaviour
{
    public ScoreManager scoremanager;
    public Vector2 velocity;
    public float gravity = -9.8f;
    public float floorY = -5f;
    public float netHeight = -3f;
    public int playerScore = 0;
    public int playerID = 0;
    public int AIScore = 0;
    public int AIID = 0;
    public float resetVx = 4f;
    public float resetVy = 4f;

    void Start()

    {
        velocity = new Vector2(resetVx, resetVy);
    }

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
            if (transform.position.x < 8 && transform.position.x > -8) // only if ball is on platform
            {
                transform.position = new Vector3(transform.position.x, floorY, 0);
                velocity.y = -velocity.y * 0.8f;
            }
        }

        // Net Colission
        if (Mathf.Abs(transform.position.x) < 0.1f && transform.position.y < netHeight)
        {
            velocity.x = -velocity.x * 0.5f;
        }

        // update score
        if (transform.position.y < -6)
        {
            if (transform.position.x > 0)
            {

                scoremanager.AddPoint(playerID);
            }
            else
            {
                scoremanager.AddPoint(AIID);
            }
        }

        // AI Logic
        if (transform.position.x > 0 && transform.position.x < 8)
        {
        // Detect if its basically contacting the floor
        if (transform.position.y <= (floorY + 0.1f) && velocity.y < 0)
        {
            // AI hits 45 towards the left
            HitBall(135f, 10f); 
        }

    }
    }
    public void HitBall(float angleDegrees, float power)
    {
        float rad = angleDegrees * Mathf.Deg2Rad;
        velocity = new Vector2(Mathf.Cos(rad) * power, Mathf.Sin(rad) * power);
    }
    
}
