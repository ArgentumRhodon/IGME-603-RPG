using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    public float ArrowSpeed = 5f;
    public float destroyTime = 3f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy();
        SetVelocity();
    }
    private void SetVelocity()
    {
        rb.velocity = transform.right * ArrowSpeed;
    }

    private void Destroy()
    {
        Destroy(gameObject, destroyTime);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
