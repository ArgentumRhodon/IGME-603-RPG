using Unity.VisualScripting;
using UnityEngine;

enum MimicState
{
    Idle,
    SeekPlayer,
    Lit,
    Exploding,
}

public class TNTMimicBehavior : MonoBehaviour
{
    private MimicState activeState;

    private GameObject player;
    [SerializeField]
    private GameObject explosionCollider;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Health health;

    // Start is called before the first frame update
    void Start()
    {
        activeState = MimicState.Idle;

        player = GameObject.FindFirstObjectByType<PlayerMovement>().gameObject;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;

        float playerHealth = player.GetComponent<PlayerHealth>().currentHealth;
        float sqrDistToPlayer = Vector2.SqrMagnitude(transform.position - player.transform.position);

        if (playerHealth <= 0)
        {
            StateExit(MimicState.Idle);
            return;
        }

        switch (activeState)
        {
            case MimicState.Idle:
                if (sqrDistToPlayer < 5 && sqrDistToPlayer > 0.01)
                {
                    StateExit(MimicState.SeekPlayer);
                }
                break;
            case MimicState.SeekPlayer:
                SeekPlayer();
                // if (sqrDistToPlayer > 5) StateExit(MimicState.Idle);
                if (sqrDistToPlayer < 0.5) StateExit(MimicState.Lit);
                break;
            case MimicState.Lit:
                break;
            case MimicState.Exploding:
                break;
        }

        if (health.currentHealth <= 0 && activeState != MimicState.Lit && activeState != MimicState.Exploding)
        {
            StateExit(MimicState.Lit);
        }
    }

    private void SeekPlayer()
    {
        Vector2 movement = (player.transform.position - transform.position).normalized * Time.deltaTime;
        this.transform.position += (Vector3)movement;

        spriteRenderer.flipX = movement.x < 0;
    }

    private void EnableExplosionCollider()
    {
        explosionCollider.SetActive(true);
    }

    private void DisableExplosionCollider()
    {
        explosionCollider.SetActive(false); 
    }

    private void StateEnter(MimicState state)
    {
        Debug.Log("Entering State: " + state);

        switch (state)
        {
            case MimicState.SeekPlayer:
                animator.SetBool("Run", true);
                break;
            case MimicState.Lit:
                animator.SetBool("Lit", true);
                Invoke("Explode", 0.5f);
                break;
            case MimicState.Exploding:
                animator.SetTrigger("Explode");
                break;
        }

        activeState = state;
    }

    private void StateExit(MimicState nextState)
    {
        switch (activeState)
        {
            case MimicState.SeekPlayer:
                animator.SetBool("Run", false);
                break;
            case MimicState.Lit:
                animator.SetBool("Lit", false);
                break;
        }

        StateEnter(nextState);
    }

    private void Explode()
    {
        StateExit(MimicState.Exploding);
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
