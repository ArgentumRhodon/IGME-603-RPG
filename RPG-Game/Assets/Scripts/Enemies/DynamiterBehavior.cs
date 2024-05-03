using Unity.VisualScripting;
using UnityEngine;

enum DynamiterState
{
    Idle,
    SeekPlayer,
    Throwing,
    Dying
}

public class DynamiterBehavior : MonoBehaviour
{
    private DynamiterState activeState;

    private GameObject player;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Health health;
    [SerializeField]
    private GameObject dynamitePrefab;
    [SerializeField]
    private float timeSinceLastThrow = 0.0f;
    private float secondsPerThrow = 2.0f;

    public int playercombo;
    public float timer;
    public int hit_kf;
    public float speed;
    public Color color;

    // Start is called before the first frame update
    void Start()
    {
        activeState = DynamiterState.Idle;

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
        if (player == null) return;

        timeSinceLastThrow += Time.deltaTime;

        float playerHealth = player.GetComponent<PlayerHealth>().currentHealth;
        Vector2 displacementToPlayer = transform.position - player.transform.position;
        float sqrDistToPlayer = Vector2.SqrMagnitude(displacementToPlayer);

        if (playerHealth <= 0)
        {
            StateEnter(DynamiterState.Idle);
            return;
        }

        switch (activeState)
        {
            case DynamiterState.Idle:
                if (sqrDistToPlayer < 35)
                {
                    StateEnter(DynamiterState.SeekPlayer);
                }
                break;
            case DynamiterState.SeekPlayer:
                SeekPlayer();
                if (sqrDistToPlayer <= 10)
                {
                    StateEnter(DynamiterState.Throwing);
                }
                // if (sqrDistToPlayer < 0.5) StateExit(DynamiterState.Lit);
                break;
            case DynamiterState.Throwing:
                if (sqrDistToPlayer > 10) StateEnter(DynamiterState.SeekPlayer);

                if(timeSinceLastThrow >= secondsPerThrow)
                {
                    animator.SetTrigger("Throw");
                    timeSinceLastThrow = 0;
                }

                spriteRenderer.flipX = displacementToPlayer.x > 0;
                break;
        }

        Debug.Log(activeState);

        if (health.currentHealth <= 0 && activeState != DynamiterState.Dying)
        {
            StateEnter(DynamiterState.Dying);
        }
        switch (playercombo)
        {
            case 0:
                break;
            case 1:
                color = new Vector4(0.8f, 0.4f, 0, 1f);
                if (timer < hit_kf && (timer + Time.deltaTime) >= hit_kf)
                {
                    hit_kf++;
                    health.ReduceHealth(2);
                    spriteRenderer.color = Color.red;
                    Invoke("ResetSpriteColor", 0.1f);
                }
                if (hit_kf > 5)
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
                speed = 0.40f;
                color = new Vector4(0, 0.5f, 1, 1);
                if (timer > 1)
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
                if(timer < 0.5)
                {
                    spriteRenderer.color = Color.yellow;
                    Invoke("ResetSpriteColor", 0.1f);
                    timer += Time.deltaTime;
                }
                
                break;
            case 4:
                if(timer < 0.5)
                {
                    spriteRenderer.color = new Vector4(0.8f, 0.4f, 0, 1f);
                    Invoke("ResetSpriteColor", 0.1f);
                    timer += Time.deltaTime;
                }
                break;
            case 5:
                speed = 0.0f;
                color = new Vector4(0, 0.5f, 1, 1);
                if (timer > 2)
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
            case 6:
                if(timer < 0.5)
                {
                    spriteRenderer.color = Color.yellow;
                    Invoke("ResetSpriteColor", 0.1f);
                    timer += Time.deltaTime;
                }   
                break;
            default:
                break;
        }
    }

    private void SeekPlayer()
    {
        Vector2 movement = (player.transform.position - transform.position).normalized * Time.deltaTime;
        this.transform.position += (Vector3)movement;

        spriteRenderer.flipX = movement.x < 0;
    }

    private void ThrowDynamite()
    {
        Dynamite dynamite = Instantiate(dynamitePrefab).GetComponent<Dynamite>();
        dynamite.transform.position = transform.position + new Vector3(0, 0.5f, -5);
        dynamite.targetPosition = player.transform.position + new Vector3(0, 0.5f, 0);
    }

    private void StateEnter(DynamiterState nextState)
    {
        Debug.Log("Entering State: " + nextState);

        switch (activeState)
        {
            case DynamiterState.SeekPlayer:
                animator.SetBool("Run", false);
                break;
            case DynamiterState.Throwing:
                animator.ResetTrigger("Throw");
                break;
        }

        switch (nextState)
        {
            case DynamiterState.SeekPlayer:
                animator.SetBool("Run", true);
                break;
            case DynamiterState.Dying:
                animator.SetTrigger("Death");
                break;
            case DynamiterState.Throwing:
                timeSinceLastThrow = 0.0f;
                animator.SetTrigger("Throw");
                break;
        }

        activeState = nextState;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"{collision.gameObject.name} collided with {gameObject.name}");

        health.ReduceHealth(collision.gameObject.GetComponent<Damage>().amount);

        spriteRenderer.color = Color.red;
        Invoke("ResetSpriteColor", 0.1f);

        if (collision.gameObject.GetComponentInParent<Power>() == null) return;

        if (collision.gameObject.GetComponentInParent<Power>().ischarge == false)
        {
            print("no charge");
            playercombo = 0;
        }
        else
        {
            playercombo = (int)collision.gameObject.GetComponentInParent<Power>().p_type * 3 + (int)collision.gameObject.GetComponentInParent<Power>().c_type + 1;
            timer = 0;
            hit_kf = 1;
            print(playercombo);
        }
    }

    private void ResetSpriteColor() => spriteRenderer.color = color;
}