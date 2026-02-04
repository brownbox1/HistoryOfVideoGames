using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public TennisPhysics ball;
    public float currentAngle = 45f;
    public KeyCode hitKey = KeyCode.Space;

    // Update is called once per frame
    void Update()
    {
        // Rotate knob w/ arrow keys
        if (Input.GetKey(KeyCode.UpArrow)) currentAngle += 50f * Time.deltaTime;
        if ((Input.GetKey(KeyCode.DownArrow)) currentAngle -= 50f * Time.deltaTime;

        // boundary for angle so it doesn't hit the ground
        currentAngle = Mathf.Clamp(currentAngle, 10f, 80f);

        // contacting ball
        if (Input.GetKeyDown(hitKey))
        {

            if (ball.transform.position.x < 0)
            {
                ball.HitBall(currentAngle, 12f);
            }
        }
    }
}
