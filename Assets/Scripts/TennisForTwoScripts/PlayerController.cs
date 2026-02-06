using UnityEngine;
using UnityEngine.InputSystem; // Add this line!

public class PlayerController : MonoBehaviour
{
    public TennisPhysics ball;
    public LineRenderer aimLine;
    
    [Header("Settings")]
    public float currentAngle = 45f;
    public float AIAngle = 45f;
    public float rotationSpeed = 60f;
    public float hitPower = 8f;

    void Update()
    {
        var keyboard = Keyboard.current;
        if (keyboard == null) return;

        // Adjust Angles
        if (keyboard.upArrowKey.isPressed)
        {
            currentAngle += rotationSpeed * Time.deltaTime;
        }
        if (keyboard.downArrowKey.isPressed)
        {
            currentAngle -= rotationSpeed * Time.deltaTime;
        }

        // Angle doesn't fire into the ground
        currentAngle = Mathf.Clamp(currentAngle, 10f, 80f);

        // When Space is pressed, make sure its properly contacting floor
        if (keyboard.spaceKey.wasPressedThisFrame)
        {
            if (ball.transform.position.x <= 0 && ball.transform.position.x >= -9) 
            {
                if (ball.transform.position.y <= (-4+ 0.7f))
                {
                    ball.HitBall(currentAngle, hitPower);
                }
            }
        }

        // line code
        // Calculate the direction
        float rad = currentAngle * Mathf.Deg2Rad;
        Vector3 dir = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0);
        
        // set the positions for the Line Renderer
        Vector3 startPos = new Vector3(-8f, -4f, 0); 
        aimLine.SetPosition(0, startPos);
        aimLine.SetPosition(1, startPos + dir * 2f);
       }

 /*

Games to Cover:
Tennis for Two
Spacewar!
Pong
Space Invaders
Pac-Man
Super Mario Bros (1-1)
Tetris
Zelda
Doom
Street Fighter
 */
}