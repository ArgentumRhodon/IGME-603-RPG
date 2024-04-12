using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float speed = 5f;
    private Vector2 movementDirection;
    public float Health = 100f;
    private Attack attack;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        //animator.SetBool("Attack", false);
        rb = GetComponent<Rigidbody2D>();
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

    void FixedUpdate()
    {
        rb.velocity = movementDirection * speed;
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
