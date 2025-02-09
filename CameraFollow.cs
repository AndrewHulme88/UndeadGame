using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] float speed = 0.125f;
    [SerializeField] float minX;
    [SerializeField] float maxX;
    [SerializeField] float minY;
    [SerializeField] float maxY;

    void Start()
    {
        transform.position = playerTransform.position;
    }

    void Update()
    {
        if(playerTransform != null)
        {
            float clampedX = Mathf.Clamp(playerTransform.position.x, minX, maxX);
            float clampedY = Mathf.Clamp(playerTransform.position.y, minY, maxY);

            transform.position = Vector2.Lerp(transform.position, new Vector2(clampedX, clampedY), speed);
        }
    }
}
