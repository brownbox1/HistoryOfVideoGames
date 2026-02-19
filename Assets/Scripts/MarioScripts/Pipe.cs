using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class Pipe : MonoBehaviour
{
    public Transform connection;
    public Camera undergroundCamera;
    public Camera mainCamera;
    public Vector3 undergroundCameraPos;
    public enum PipeDirection
    {
        Down,
        Right,
        Left,
        Up
    }

    public PipeDirection direction = PipeDirection.Down;

    private bool isPlayerTouching = false;
    private bool isEntering = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerTouching = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerTouching = false;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (isEntering || !isPlayerTouching || connection == null)
        {
            return;
        }
        var keyboard = Keyboard.current;
        bool inputPressed = false;
        if (direction == PipeDirection.Down && keyboard.sKey.isPressed)
        {
            inputPressed = true;
        }
        else if (direction == PipeDirection.Right && keyboard.dKey.isPressed)
        {
            inputPressed = true;
        }

        if (inputPressed)
        {
            isEntering = true;
            StartCoroutine(EnterPipeSequence(GameObject.FindGameObjectWithTag("Player")));
            if (direction == PipeDirection.Right)
            {
                mainCamera.enabled = true;
                undergroundCamera.enabled = false;
            }
            else
            {
                undergroundCamera.enabled = true;
                mainCamera.enabled = false;
            }
        }
    }

    IEnumerator EnterPipeSequence(GameObject player)
    {
        player.GetComponent<PlayerMovement>().enabled = false;
        Rigidbody2D rigidbody = player.GetComponent<Rigidbody2D>();
        rigidbody.linearVelocity = Vector2.zero;
        rigidbody.isKinematic = true;

        // figure out what direction to slide in
        Vector3 moveDir = Vector3.down; // default
        if (direction == PipeDirection.Right)
        {
            moveDir = Vector3.right;
        }

        Vector3 endPosition = player.transform.position + moveDir * 3f;
        float elapsed = 0f;
        float duration = 1f;

        while (elapsed < duration)
        {
            player.transform.position = Vector3.Lerp(player.transform.position, endPosition, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        player.transform.position = connection.position;

        rigidbody.isKinematic = false;
        player.GetComponent<PlayerMovement>().enabled = true;

    }
}
