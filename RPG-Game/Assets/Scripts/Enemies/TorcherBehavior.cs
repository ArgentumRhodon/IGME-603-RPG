using UnityEngine;

enum State
{
    Idle,
    SeekPlayer,
    Attack,
    Dying
}

public class TorcherBehavior : MonoBehaviour
{
    public int damage = 10;

    private State activeState;

    private GameObject player;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Health health;

    [SerializeField]
    private GameObject torchCollider;

    // Start is called before the first frame update
    void Start()
    {
        activeState = State.Idle;

        player = GameObject.FindFirstObjectByType<PlayerMovement>().gameObject;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null) return; 

        float playerHealth = player.GetComponent<PlayerHealth>().currentHealth;
        float sqrDistToPlayer = Vector2.SqrMagnitude(transform.position - player.transform.position);

        if(playerHealth <= 0)
        {
            StateExit(State.Idle);
            return;
        }

        switch (activeState)
        {
            case State.Idle:
                if (sqrDistToPlayer < 25 && sqrDistToPlayer > 0.01)
                {
                    StateExit(State.SeekPlayer);
                }
                break;
            case State.SeekPlayer:
                SeekPlayer();
                if (sqrDistToPlayer > 25) StateExit(State.Idle);
                if (sqrDistToPlayer < 0.5) StateExit(State.Attack);
                break;
            case State.Dying:
                break;
        }

        if(health.currentHealth <= 0)
        {
            StateExit(State.Dying);
        }
    }

    private void SeekPlayer()
    {
        Vector2 movement = (player.transform.position - transform.position).normalized * Time.deltaTime;
        this.transform.position += (Vector3)movement;

        spriteRenderer.flipX = movement.x < 0;
    }

    private void EnableTorchCollider()
    {
        torchCollider.SetActive(true);

        if (spriteRenderer.flipX)
        {
            torchCollider.transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            torchCollider.transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }
    }

    private void DisableTorchCollider()
    {
        torchCollider.SetActive(false);
    }

    private void StateEnter(State state)
    {
        switch (state)
        {
            case State.SeekPlayer:
                animator.SetBool("Run", true);
                break;
            case State.Attack:
                animator.SetTrigger("Attack");
                break;
        }

        activeState = state;
    }

    private void StateExit(State nextState)
    { 
        switch (activeState) 
        {
            case State.SeekPlayer:
                animator.SetBool("Run", false);
                break;
        }

        StateEnter(nextState);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"{collision.gameObject.name} collided with {gameObject.name}");

        health.ReduceHealth(collision.gameObject.GetComponent<Damage>().amount);

        spriteRenderer.color = Color.red;
        Invoke("ResetSpriteColor", 0.1f);
    }

    private void ResetSpriteColor() => spriteRenderer.color = Color.white;
}
