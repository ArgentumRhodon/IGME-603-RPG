using UnityEngine;

enum TorcherState
{
    Idle,
    SeekPlayer,
    Attack,
    Dying
}

public class TorcherBehavior : MonoBehaviour
{
    private TorcherState activeState;

    public GameObject player;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Health health;
    private PlayerControlManager playerControlManager;

    [SerializeField]
    private GameObject torchCollider;

    // Start is called before the first frame update
    void Start()
    {
        activeState = TorcherState.Idle;

        //player = GameObject.FindFirstObjectByType<PlayerMovement>().gameObject;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();
        playerControlManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerControlManager>();
        player = playerControlManager.currentPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        player = playerControlManager.currentPlayer;
        if (player == null) return; 

        float playerHealth = player.GetComponent<PlayerHealth>().currentHealth;
        float sqrDistToPlayer = Vector2.SqrMagnitude(transform.position - player.transform.position);

        if(playerHealth <= 0)
        {
            StateExit(TorcherState.Idle);
            return;
        }

        switch (activeState)
        {
            case TorcherState.Idle:
                if (sqrDistToPlayer < 25 && sqrDistToPlayer > 0.01)
                {
                    StateExit(TorcherState.SeekPlayer);
                }
                break;
            case TorcherState.SeekPlayer:
                SeekPlayer();
                // if (sqrDistToPlayer > 25) StateExit(TorcherState.Idle);
                if (sqrDistToPlayer < 0.5) StateExit(TorcherState.Attack);
                break;
            case TorcherState.Dying:
                break;
        }

        if(health.currentHealth <= 0 && activeState != TorcherState.Dying)
        {
            StateExit(TorcherState.Dying);
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

    private void StateEnter(TorcherState state)
    {
        switch (state)
        {
            case TorcherState.SeekPlayer:
                animator.SetBool("Run", true);
                break;
            case TorcherState.Attack:
                animator.SetTrigger("Attack");
                break;
            case TorcherState.Dying:
                animator.SetTrigger("Death");
                break;
        }

        activeState = state;
    }

    private void StateExit(TorcherState nextState)
    { 
        switch (activeState) 
        {
            case TorcherState.SeekPlayer:
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
