using UnityEngine;

public class SideScrolling : MonoBehaviour
{
    private Transform player;

    void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    void LateUpdate()
    {
        Vector3 CameraPosition = transform.position;
        CameraPosition.x = Mathf.Max(player.position.x, CameraPosition.x);
        transform.position = CameraPosition;
    }


}
