using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    public Vector2 movementVector = Vector2.zero;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(movementVector.magnitude > 0)
        {
            animator.SetBool("Run", true);
            if(movementVector.x != 0)
            {
                spriteRenderer.flipX = movementVector.x < 0;
            }
        }
        else
        {
            animator.SetBool("Run", false);
        }
    }

    private void FixedUpdate()
    {
        transform.position += (Vector3)movementVector * Time.fixedDeltaTime * speed;
    }

    private void OnMove(InputValue value)
    {
        movementVector = value.Get<Vector2>();
    }
}
