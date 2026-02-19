using UnityEngine;

public class NormalCoin : MonoBehaviour
{
    private bool isCollected = false;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isCollected)
        {
            isCollected = true;
            GameManager.Instance.addScore(200);
            Destroy(gameObject);
        }
    }
}
