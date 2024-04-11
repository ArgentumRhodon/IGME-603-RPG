using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float speed = 5f;
    private Vector2 movementDirection;
    public float Health = 100f;
    private Attack attack;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        //animator.SetBool("Attack", false);
        rb = GetComponent<Rigidbody2D>();
        attack = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Attack>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    void FixedUpdate()
    {
        rb.velocity = movementDirection * speed;
    }

    public void TakeDamage(float damage)
    {
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
            animator.SetBool("Attack", true);
            TakeDamage(attack.damage);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            animator.SetBool("Attack", false);
        }
    }
}
