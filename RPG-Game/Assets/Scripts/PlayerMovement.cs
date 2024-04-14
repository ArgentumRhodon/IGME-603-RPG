using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb = null;
    private float speed = 5f;
    private Vector2 movementVector = Vector2.zero;
    private Vector2 movementDirection = Vector2.zero;
    public float Health = 100f;
    private Attack attack;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private PlayerMovementControls movment = null;

    private void Awake()
    {
        movment = new PlayerMovementControls();
        rb = GetComponent<Rigidbody2D>();
        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;
    }

    private void OnEnable()
    {
        movment.Enable();
        movment.Player.Movment.performed += OnMovementPerformed;
        movment.Player.Movment.canceled += OnMovementCancelled;
    }

    private void OnDisable()
    {
        movment.Disable();
        movment.Player.Movment.performed -= OnMovementPerformed;
        movment.Player.Movment.canceled -= OnMovementCancelled;
    }

    // Start is called before the first frame update
    void Start()
    {
        attack = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Attack>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if(movementDirection.magnitude > 0)
        {
            animator.SetBool("Run", true);
            if(movementDirection.x != 0)
            {
                spriteRenderer.flipX = movementDirection.x < 0;
            }
        }
        else
        {
            animator.SetBool("Run", false);
        }

        if(Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attack");
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = movementVector * speed;
    }

    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
        movementVector = value.ReadValue<Vector2>();
    }

    private void OnMovementCancelled(InputAction.CallbackContext value)
    {
        movementVector = Vector2.zero;
    }
    public void TakeDamage(float damage)
    {
        Debug.Log("Taking Damage!");
        Health -= damage;
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            TakeDamage(attack.damage);
        }
    }
}
