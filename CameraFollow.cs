using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] float speed = 0.125f;

    void Start()
    {
        transform.position = playerTransform.position;
    }

    void Update()
    {
        if(playerTransform != null)
        {
            transform.position = Vector2.Lerp(transform.position, playerTransform.position, speed);
        }
    }
}
