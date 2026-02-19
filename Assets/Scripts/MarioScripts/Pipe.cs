using UnityEngine;
using System.Collections;

public class Pipe : MonoBehaviour
{
    public Transform connection;
    public KeyCode enterKey = KeyCode.S;
    private bool isPlayerOnTop = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerOnTop = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerOnTop = false;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (isPlayerOnTop && Input.GetKeyDown(enterKey) && connection != null)
        {
            StartCoroutine(EnterPipeSequence(GameObject.FindGameObjectWithTag("Player")));
        }
    }

    IEnumerator EnterPipeSequence(GameObject player)
    {
        player.GetComponent<PlayerMovement>().enabled = false;
        Rigidbody2D rigidbody = player.GetComponent<Rigidbody2D>();
        rigidbody.linearVelocity = Vector2.zero;
        rigidbody.isKinematic = true;

        Vector3 endPosition = player.transform.position + Vector3.down * 2f;
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
