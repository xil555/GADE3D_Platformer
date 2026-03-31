using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Health")]
    public Slider healthBar;
    public TMP_Text healthText;
    public int health = 100;
    public int maxHealth = 100;

    [Header("Lives")]
    public int maxLives = 3;
    public int lives = 3;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip healthPickupSound;
    public AudioClip damageSound;
    public AudioClip deathSound;
    public AudioClip gemSound;

    [Header("Spawn")]
    public Transform startPoint;
    [SerializeField] private ResetTrigger resetTrigger;

    [Header("Movement")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpSpeed = 8f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private Transform cameraTransform;

    private PlayerInputController playerInputController;
    private GroundController groundController;
    private Rigidbody rb;
    private bool jumpTriggered;
    private bool isDying;

    private MyStack<CheckpointData> checkpointStack = new MyStack<CheckpointData>();
    private void Awake()
    {
        playerInputController = GetComponent<PlayerInputController>();
        groundController = GetComponent<GroundController>();
        rb = GetComponent<Rigidbody>();

        if (cameraTransform == null && Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }

        if (playerInputController != null)
        {
            playerInputController.OnJumpedBttonPressed += JumpButtonPressed;
        }
    }

    private void Start()
    {
        maxHealth = health;
        lives = maxLives;

        if (resetTrigger != null && startPoint != null)
        {
            resetTrigger.SetSpawnPoint(startPoint.position);
        }
    }

    private void Update()
    {
        if (healthText != null)
        {
            healthText.text = health + " / " + maxHealth;
        }

        if (healthBar != null && maxHealth > 0)
        {
            healthBar.value = (float)health / maxHealth;
        }
    }

    private void FixedUpdate()
    {
        if (playerInputController == null || rb == null)
        {
            return;
        }

        Vector2 input = playerInputController.MovementInputVector;
        Vector3 moveDirection;

        if (cameraTransform != null)
        {
            Vector3 camForward = cameraTransform.forward;
            Vector3 camRight = cameraTransform.right;
            camForward.y = 0f;
            camRight.y = 0f;
            camForward.Normalize();
            camRight.Normalize();
            moveDirection = camForward * input.y + camRight * input.x;
        }
        else
        {
            moveDirection = new Vector3(input.x, 0f, input.y);
        }

        Vector3 velocity = moveDirection * speed;
        velocity.y = rb.linearVelocity.y;

        if (jumpTriggered)
        {
            velocity.y = jumpSpeed;
            jumpTriggered = false;
        }

        rb.linearVelocity = velocity;

        if (moveDirection.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
        }
    }
    public void SaveCheckpoint(Vector3 newPosition)
    {
        if (!checkpointStack.IsEmpty())
        {
            checkpointStack.Pop();

            checkpointStack.Push(new CheckpointData(newPosition, lives));
        }
        if (resetTrigger != null)
        {
            resetTrigger.SetSpawnPoint(newPosition);
        }
    }
    private void JumpButtonPressed()
    {
        if (groundController != null && groundController.IsGrounded)
        {
            jumpTriggered = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gems"))
        {
            if (audioSource != null && gemSound != null)
            {
                audioSource.PlayOneShot(gemSound);
            }
            return;
        }

        if (other.CompareTag("Death"))
        {
            if (audioSource != null && deathSound != null)
            {
                audioSource.PlayOneShot(deathSound);
            }

            KillPlayer();
            return;
        }

        if (other.CompareTag("Damage"))
        {
            TakeDamage(15);
            return;
        }

        if (other.CompareTag("HealthPickUp"))
        {
            if (health < maxHealth)
            {
                health += 25;
                health = Mathf.Min(health, maxHealth);

                other.gameObject.SetActive(false);

                if (audioSource != null && healthPickupSound != null)
                {
                    audioSource.PlayOneShot(healthPickupSound);
                }

                StartCoroutine(RespawnPickup(other.gameObject, 5f));
            }
        }
    }

    public void TakeDamage(int amount)
    {
        if (isDying)
        {
            return;
        }

        health -= amount;

        if (audioSource != null && damageSound != null)
        {
            audioSource.PlayOneShot(damageSound);
        }

        if (health <= 0)
        {
            health = 0;
            KillPlayer();
        }
    }

    private void KillPlayer()
    {
        if (isDying)
        {
            return;
        }

        isDying = true;
        lives--;

        if (lives > 0)
        {
            RespawnAtCurrentCheckpoint();
        }
        else
        {
            FullResetToStart();
        }

        health = maxHealth;
        isDying = false;
    }

    private void RespawnAtCurrentCheckpoint()
    {
        if (resetTrigger != null)
        {
            resetTrigger.RespawnPlayer(gameObject);
        }
        else
        {
            transform.position = startPoint != null ? startPoint.position : transform.position;
        }
    }

    private void FullResetToStart()
    {
        lives = maxLives;

        if (startPoint != null && resetTrigger != null)
        {
            resetTrigger.SetSpawnPoint(startPoint.position);
            resetTrigger.RespawnPlayer(gameObject);
        }
        else if (startPoint != null)
        {
            transform.position = startPoint.position;
        }

        ResetGemCount();
    }

    private void ResetGemCount()
    {
        Coins.RestoreAllPickups();

        PlayerInventory inventory = GetComponent<PlayerInventory>();
        if (inventory != null)
        {
            inventory.ResetCoins();
        }
    }

    private IEnumerator RespawnPickup(GameObject pickup, float delay)
    {
        yield return new WaitForSeconds(delay);
        pickup.SetActive(true);
    }

    public void AddHealth(int addHealth)
    {
        health += addHealth;
        health = Mathf.Clamp(health, 0, maxHealth);
    }
}
