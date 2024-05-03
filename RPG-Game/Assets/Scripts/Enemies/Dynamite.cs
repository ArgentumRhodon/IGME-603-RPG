using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamite : MonoBehaviour
{
    public Vector2 targetPosition = Vector2.zero;
    public float speed = 1.0f;

    private Animator animator;
    [SerializeField]
    private GameObject explosionCollider;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 displacement = (Vector2)targetPosition - (Vector2)transform.position;
        Vector2 velocity = (Vector2)displacement.normalized * speed * Time.deltaTime;

        if(targetPosition != Vector2.zero)
        {
            transform.position += (Vector3)velocity;
        }

        if (displacement.magnitude < 0.1f)
        {
            animator.SetTrigger("Explode");
        }
    }

    private void EnableExplosionCollider()
    {
        explosionCollider.SetActive(true);
    }

    private void DisableExplosionCollider()
    {
        explosionCollider.SetActive(false);
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
