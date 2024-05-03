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

    public int playercombo;
    public float timer;
    public bool iced;
    public float speed;
    private Color color;


    // Start is called before the first frame update
    void Start()
    {
        activeState = MimicState.Idle;

        player = GameObject.FindFirstObjectByType<PlayerMovement>().gameObject;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();

        playercombo = 0;
        timer = 0;
        speed = 1;
        color = Color.white;
        iced = false;
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
                if (sqrDistToPlayer < 0.5 && !iced) StateExit(MimicState.Lit);
                break;
            case MimicState.Lit:
                break;
            case MimicState.Exploding:
                break;
        }

        if (health.currentHealth <= 0 && activeState != MimicState.Lit && activeState != MimicState.Exploding && !iced)
        {
            StateExit(MimicState.Lit);
        }
        switch (playercombo)
        {
            case 0:
                break;
            case 1:
                StateExit(MimicState.Lit);
                break;
            case 2:
                //activeState = MimicState.SeekPlayer;
                speed = 0.40f;
                color = new Vector4(0, 0.5f, 1, 1);
                if (timer > 1)
                {
                    iced = false;
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
                spriteRenderer.color = Color.yellow;
                Invoke("ResetSpriteColor", 0.1f);
                break;
            case 4:
                StateExit(MimicState.Lit);
                break;
            case 5:
                speed = 0.00f;
                color = new Vector4(0, 0.5f, 1, 1);
                if (timer > 1)
                {
                    iced = false;
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
                spriteRenderer.color = Color.yellow;
                Invoke("ResetSpriteColor", 0.1f);
                break;
            default:
                break;
        }
    }

    private void SeekPlayer()
    {
        Vector2 movement = (player.transform.position - transform.position).normalized * Time.deltaTime;
        this.transform.position += speed * (Vector3)movement;

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
        if(activeState == MimicState.Exploding || activeState == MimicState.Lit)
        {
            return;
        }

        Debug.Log($"{collision.gameObject.name} collided with {gameObject.name}");

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
            //timer = 0;
            //hit_kf = 1;
            print("charge = " + playercombo);
        }
        if(playercombo != 2 && playercombo != 5)
        {
            health.ReduceHealth(collision.gameObject.GetComponent<Damage>().amount);
        }
        else
        {
            iced = true;
            print("Iced");
        }
    }

    private void ResetSpriteColor() => spriteRenderer.color = color;
}
