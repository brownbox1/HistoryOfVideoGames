using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private BoxCollider2D boxCollider2D;
    void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.ResetLevel();
        }
    }
}
