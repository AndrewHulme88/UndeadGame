using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] Transform shotPoint;
    [SerializeField] float timeBetweenShots = 0.3f;

    private float shotTime = 0;

    void Update()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle - 90);
        transform.rotation = rotation;

        if(Input.GetMouseButtonDown(0) && Time.time >= shotTime)
        {
            Instantiate(projectile, shotPoint.position, transform.rotation);
            shotTime = Time.time + timeBetweenShots;
        }
    }

}
