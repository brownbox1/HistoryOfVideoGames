using UnityEngine;
using System.Collections;

public class FlagPole : MonoBehaviour
{
    public Transform flag; // Flag Sprite
    public Transform flagBottom; // Sprite of the bottom of the pole
    public Transform castleEntrance; // castle door
    public float slideSpeed = 5f;

    private bool isFinished = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isFinished)
        {
            isFinished = true;
            StartCoroutine(FinishLevelSequence(collision.gameObject));
        }
    }

    IEnumerator FinishLevelSequence(GameObject player)
    {
        // freeze player
        PlayerMovement movement = player.GetComponent<PlayerMovement>();
        Rigidbody2D rigidbody = player.GetComponent<Rigidbody2D>();
        movement.enabled = false;
        rigidbody.linearVelocity = Vector2.zero;
        rigidbody.isKinematic = true;

        // slide player and flag down
        Vector3 playerEndPos = new Vector3(transform.position.x, flagBottom.position.y+1, 0);

        while (Vector3.Distance(player.transform.position, playerEndPos) > 0.1f)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, playerEndPos, slideSpeed * Time.deltaTime);
            flag.position = Vector3.MoveTowards(flag.position, flagBottom.position, slideSpeed*Time.deltaTime);
            yield return null;
        }

        // hop off the flag pole
        player.transform.position = new Vector3(transform.position.x + 0.5f, player.transform.position.y+0.5f, 0);
        GameManager.Instance.TimeBonus();
        yield return new WaitForSeconds(0.5f);

        // walk to the castle
        float walkSpeed = 3f;
        while (Vector3.Distance(player.transform.position, castleEntrance.position) > 0.1f)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, castleEntrance.position, walkSpeed * Time.deltaTime);
            yield return null;
        }

        player.SetActive(false);
        Debug.Log("Level Completed.");

        yield return new WaitForSeconds(2f);
        GameManager.Instance.ResetLevel(); // CHANGE THIS TO ENDING SEQUENCE OR MAIN MENU OR SOMETHING
        {
            
        }
    }
}
