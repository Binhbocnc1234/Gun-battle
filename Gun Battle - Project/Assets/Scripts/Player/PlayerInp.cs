using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInputHandl : MonoBehaviour
{
    //Player state
    private bool isShooting = false;
    private bool isJumping = false;
    //Stats
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    private float lastTapTime = 0f;
    private float doubleTapThreshold = 0.3f;  // Time window for double tap
    //Link to other gameObjects
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    private Rigidbody2D rb;
    private Vector2 movementInput;
    private Vector2 shootDirection = Vector2.up;  // Default shooting direction
    private bool canMove = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // InputAction callbacks
    public void OnMove(InputAction.CallbackContext context)
    {
        if (isShooting)
        {
            HandleShootingMovement(context);
        }
        else
        {
            movementInput = context.ReadValue<Vector2>();
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && !isShooting && !isJumping)
        {
            Jump();
        }
    }

    public void OnLootOrSitDown(InputAction.CallbackContext context)
    {
        if (context.performed && !isShooting)
        {
            SitOrLoot();
        }
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (!isShooting)
            {
                EnterShootingPhase();
            }
            else
            {
                if (context.performed) Shoot();
                if (context.canceled) StopShooting();
            }
        }
    }

    private void EnterShootingPhase()
    {
        isShooting = true;
        canMove = false;
        Debug.Log("Entered Shooting Phase");
    }

    private void ExitShootingPhase()
    {
        isShooting = false;
        canMove = true;
        Debug.Log("Exited Shooting Phase");
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        isJumping = true;
    }

    private void SitOrLoot()
    {
        Debug.Log("Looting or sitting down");
    }

    private void HandleShootingMovement(InputAction.CallbackContext context)
    {
        Vector2 directionInput = context.ReadValue<Vector2>();
        
        // W/S adjust shooting direction
        if (directionInput.y > 0) shootDirection = Vector2.up;
        if (directionInput.y < 0) shootDirection = Vector2.down;

        // A/D adjust player's face direction
        if (context.started && directionInput.x != 0)
        {
            // Detect double-tap to exit shooting phase
            float currentTime = Time.time;
            if (currentTime - lastTapTime < doubleTapThreshold)
            {
                ExitShootingPhase();
            }
            else
            {
                lastTapTime = currentTime;
            }

            if (directionInput.x > 0) FaceRight();
            else if (directionInput.x < 0) FaceLeft();
        }
    }

    private void FaceRight()
    {
        transform.localScale = new Vector3(1, 1, 1); // Face right
        Debug.Log("Facing right");
    }

    private void FaceLeft()
    {
        transform.localScale = new Vector3(-1, 1, 1); // Face left
        Debug.Log("Facing left");
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = shootDirection * 10f;
        Debug.Log("Shooting bullet");
    }

    private void StopShooting()
    {
        Debug.Log("Stopped shooting");
    }

    private void FixedUpdate()
    {
        if (!isShooting && canMove)
        {
            rb.velocity = new Vector2(movementInput.x * moveSpeed, rb.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }
}
