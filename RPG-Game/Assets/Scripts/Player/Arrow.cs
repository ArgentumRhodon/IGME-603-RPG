using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    public float ArrowSpeed = 5f;
    public float destroyTime = 3f;
    public Vector2 velocity = Vector2.zero;
    PlayerMovement PM;
    void Start()
    {
        PM = GetComponentInParent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
        Destroy();
        SetVelocity();
    }
    private void SetVelocity()
    {
        //rb.velocity = transform.right * ArrowSpeed;
        velocity = transform.right * ArrowSpeed;
    }

    private void Destroy()
    {
        Destroy(gameObject, destroyTime);
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.position += (Vector3)velocity * Time.fixedDeltaTime;
        transform.position -= (Vector3)PM.movementVector* Time.fixedDeltaTime* PM.speed;
    }
    void Update()
    {
        
    }
}
