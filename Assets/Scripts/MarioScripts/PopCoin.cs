using UnityEngine;

public class PopCoin : MonoBehaviour
{
    public GameManager gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, 5f);
        Destroy(gameObject, 0.5f);
        GameManager.Instance.addScore(200);
    }
}
