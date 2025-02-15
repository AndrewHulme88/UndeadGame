using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] int health = 3;
    [SerializeField] Image[] hearts;
    [SerializeField] Sprite fullHeart;
    [SerializeField] Sprite emptyHeart;
    [SerializeField] float invincibilityDuration = 1f;
    [SerializeField] ParticleSystem dustParticles;

    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator anim;
    private ScreenShake screenShake;
    private bool isInvincible = false;
    private SceneTransitions sceneTransitions;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        UpdateHealthUI(health);
        screenShake = FindFirstObjectByType<ScreenShake>();
        sceneTransitions = FindFirstObjectByType<SceneTransitions>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if(movement != Vector2.zero)
        {
            anim.SetBool("isWalking", true);
            
            if(!dustParticles.isPlaying)
            {
                dustParticles.Play();
            }
        }
        else
        {
            anim.SetBool("isWalking", false);

            if(dustParticles.isPlaying)
            {
                dustParticles.Stop();
            }
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = movement.normalized * moveSpeed;
    }

    public void TakeDamage(int damageAmount)
    {
        if(isInvincible)
        {
            return;
        }

        anim.SetTrigger("hit");
        screenShake.TriggerShake();
        health -= damageAmount;
        UpdateHealthUI(health);

        if (health <= 0)
        {
            Destroy(gameObject);
            sceneTransitions.LoadScene("GameOver");
        }
        else
        {
            StartCoroutine(InvincibilityFrames());
        }
    }

    private IEnumerator InvincibilityFrames()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibilityDuration);
        isInvincible = false;
    }

    private void UpdateHealthUI(int currentHealth)
    {
        for(int i = 0; i < hearts.Length; i++)
        {
            if(i < currentHealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }

    public void Heal(int healAmount)
    {
        if(health + healAmount > 3)
        {
            health = 3;
        }
        else
        {
            health += healAmount;
        }

        UpdateHealthUI(health);
    }
}
