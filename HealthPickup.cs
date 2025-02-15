using System.Collections;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    private PlayerController player;
    [SerializeField] int healAmount = 1;
    [SerializeField] float destroyTime = 5f;
    [SerializeField] GameObject particles;
    [SerializeField] GameObject fadeAnimationObject;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        StartCoroutine(DestroyPickup());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            player.Heal(healAmount);
            Instantiate(particles, transform.position, Quaternion.identity);
            Instantiate(fadeAnimationObject, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private IEnumerator DestroyPickup()
    {
        yield return new WaitForSeconds(destroyTime);
        Instantiate(particles, transform.position, Quaternion.identity);
        Instantiate(fadeAnimationObject, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
