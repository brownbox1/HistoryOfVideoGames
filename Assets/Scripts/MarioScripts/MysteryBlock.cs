using UnityEngine;
using System.Collections;

public class MysteryBlock : MonoBehaviour
{
    public Sprite emptyBlockSprite;

    public GameObject itemToSpawn;
    public float bounceHeight = 0.2f;
    public float bounceSpeed = 10f;

    private bool isUsed = false;
    private Vector3 originalPosition;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        originalPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void RequestHit()
    {
        if (isUsed)
        {
            return; // already hit, do nothing
        }

        isUsed = true;
        spriteRenderer.sprite = emptyBlockSprite;

        if (itemToSpawn != null)
        {
            Instantiate(itemToSpawn, transform.position, Quaternion.identity);
        }

        StartCoroutine(BounceSequence());
    }

    System.Collections.IEnumerator BounceSequence()
    {
        // moving up
        Vector3 targetPos = originalPosition + (Vector3.up * bounceHeight);
        while (transform.position.y < targetPos.y)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, bounceSpeed * Time.deltaTime);
            yield return null;
        }

        while (transform.position.y > originalPosition.y)
        {
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, bounceSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = originalPosition;
    }

}
