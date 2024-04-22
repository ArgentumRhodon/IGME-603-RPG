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

    private GameObject player;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Health health;
    public int playercombo;
    public float timer;
    public int hit_kf;
    public float speed;
    private Color color;
    [SerializeField]
    private GameObject torchCollider;

    // Start is called before the first frame update
    void Start()
    {
        activeState = TorcherState.Idle;

        player = GameObject.FindFirstObjectByType<PlayerMovement>().gameObject;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();
        playercombo = 0;
        timer = 0;
        speed = 1;
        color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null) return; 

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
        switch (playercombo)
        {
            case 0:
                break;
            case 1:
                color = new Vector4(1,0.5f,0,1);
                if(timer < hit_kf && (timer + Time.deltaTime) >= hit_kf)
                {
                    hit_kf ++;
                    health.ReduceHealth(2);
                }
                if(hit_kf > 5)
                {
                    playercombo = 0;
                    timer = 0;
                    color = Color.white;
                    spriteRenderer.color = color;
                    break;
                }
                timer += Time.deltaTime;
                break;
            case 2:
                speed = 0.75f;
                color = new Vector4(0, 0.5f, 1, 1);
                if(timer > 1)
                {
                    playercombo = 0;
                    timer = 0;
                    speed = 1;
                    color = Color.white;
                    spriteRenderer.color = color;
                    break;
                }
                timer += Time.deltaTime;
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            default:
                break;
        }

    }

    private void SeekPlayer()
    {
        Vector2 movement = speed * (player.transform.position - transform.position).normalized * Time.deltaTime;
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

        if (collision.gameObject.GetComponent<Power>().ischarge == false)
        {
            print("no charge");
            playercombo = 0;
        }  
        else
        {
            playercombo = (int)collision.gameObject.GetComponent<Power>().p_type * 3 + (int)collision.gameObject.GetComponent<Power>().c_type + 1;
            timer = 0;
            hit_kf = 1;
            print(playercombo);
        }

        spriteRenderer.color = Color.red;
        Invoke("ResetSpriteColor", 0.1f);
    }

    private void ResetSpriteColor() => spriteRenderer.color = color;
}
