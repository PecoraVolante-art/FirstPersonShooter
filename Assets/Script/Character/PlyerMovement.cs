using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    CharacterController CC;
    public float maxHealth = 100f;
    private float currentHealth;
    public float speed = 12F;
    public float gravity = -9.8f;
    public bool isDead = false;
    [SerializeField]private HealthBar healthBar;
    public float jumpHeight;
    Vector3 Velocity;
    public GameObject GameOverPanel;
    bool GroundedPlayer;
    
    void Start()
    {
        GameOverPanel.SetActive(false);
        healthBar.SetMaxHealth(maxHealth);
        CC = GetComponent<CharacterController>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) return;

        GroundedPlayer = CC.isGrounded;

        if (GroundedPlayer && Velocity.y < 0)
        {
            Velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if (Input.GetButtonDown("Jump") && GroundedPlayer)
        {
            Velocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }

        Velocity.y += gravity * Time.deltaTime;
        CC.Move((move * speed + Velocity) * Time.deltaTime);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        Debug.Log("Player HP: " + currentHealth);
          healthBar.SetCurrentHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player morto");
        CC.enabled = false;
        isDead = true;
        if (GestioneSFX.Instance != null)
            GestioneSFX.Instance.PlaySFX(GestioneSFX.Instance.death);
        GameOverPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f; 

    }
}
