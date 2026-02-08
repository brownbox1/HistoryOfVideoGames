using UnityEngine;
using System.Collections;

public class TennisPhysics : MonoBehaviour
{
    public ScoreManager scoremanager;
    private bool isBallActive = true;
    public Vector2 velocity;
    public float gravity = -9.8f;
    public float floorY = -4f;
    public float netHeight = -3f;
    public int playerScore = 0;
    public int playerID = 0;
    public int AIScore = 0;
    public int AIID = 1;
    public float resetVx = 4f;
    public float resetVy = 4f;
    public int activeBounces = 0;
    public bool onPlayerSide = false;
    private bool previousSide = false;

    void Start()

    {
        velocity = new Vector2(resetVx, resetVy);
    }

    // Update is called once per frame
    void Update()
    {
        // chunk to figure out what side the ball is on
        if (transform.position.x < 0)
        {
            onPlayerSide = true;
        }
        else
        { onPlayerSide = false;}
        if (onPlayerSide != previousSide)
        {
            activeBounces = 0;
        }
        previousSide = onPlayerSide;

        // Add gravity to vertical Velo
        velocity.y += gravity * Time.deltaTime;

        // change position based on Velo
        transform.position += (Vector3)velocity * Time.deltaTime;

        // floor bounces
        if (transform.position.x <= 0 && transform.position.x >= -9) // only if ball is on platform
        {
            if (transform.position.y <= floorY)
            {
                transform.position = new Vector3(transform.position.x, floorY, 0);
                velocity.y = -velocity.y * 0.8f;
                activeBounces++;
            }
        }

        // Net Colission
        if (Mathf.Abs(transform.position.x) < 0.1f && transform.position.y < netHeight)
        {
            velocity.x = -velocity.x * 0.5f;
            if (transform.position.x > 0)
            {
                transform.position = new Vector3(transform.position.x + 0.3f, transform.position.y, 0);
    
            }
            else
            {
                transform.position = new Vector3(transform.position.x - 0.3f, transform.position.y, 0);
            }
            }

        // update score
        if (isBallActive && (transform.position.y < -6 || activeBounces >= 2))
        {
            StartCoroutine(ScoreHandler());
        }



        // AI Logic
        if (transform.position.x > 0 && transform.position.x <= 9)
        {
        // Detect if its basically contacting the floor
        if (transform.position.y <= (floorY + 0.1f) && velocity.y < 0)
        {
            // AI hits 45 towards the left
            HitBall(135f, 10f);
            activeBounces++;
        }

    }
    }
    public void HitBall(float angleDegrees, float power)
    {
        float rad = angleDegrees * Mathf.Deg2Rad;
        velocity = new Vector2(Mathf.Cos(rad) * power, Mathf.Sin(rad) * power);
    }
    
    IEnumerator ScoreHandler()
    {
        isBallActive = false;
        if (!onPlayerSide)
            {
                if (activeBounces == 0)
                {
                    scoremanager.AddPoint(1);
                }
                else
                {
                    scoremanager.AddPoint(0);
                } 
            }   
        else
            { scoremanager.AddPoint(1); }

        yield return new WaitForSeconds(2f);
        velocity = new Vector2(0, 0);
        transform.position = Vector3.zero;
        isBallActive = true;
        resetVx *= -1;
        resetVy *= -1;
        velocity = new Vector2(resetVx, resetVy);
        activeBounces = 0;

    }
}
