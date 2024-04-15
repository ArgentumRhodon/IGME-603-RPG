using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float damage = 10f;
    Rigidbody2D rb;
    GameObject player;
    SpriteRenderer spriteRenderer;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindFirstObjectByType<PlayerMovement>().gameObject;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float sqrDistToPlayer = Vector2.SqrMagnitude(transform.position - player.transform.position);

        if(sqrDistToPlayer < 25 && sqrDistToPlayer > 0.01)
        {
            SeekPlayer();
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }

        if(sqrDistToPlayer < 1)
        {
            animator.SetTrigger("Attack");
        }
    }

    void SeekPlayer()
    {
        Vector2 vel = (player.transform.position - transform.position).normalized * Time.deltaTime;
        this.transform.position += (Vector3)vel;
        
        spriteRenderer.flipX = vel.x < 0;
    }
}
