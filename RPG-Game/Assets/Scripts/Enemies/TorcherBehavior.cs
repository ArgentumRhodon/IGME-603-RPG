using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

enum State
{
    Idle,
    SeekPlayer,
    Attack
}

public class TorcherBehavior : MonoBehaviour
{
    public int damage = 10;

    private State activeState;

    private GameObject player;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    [SerializeField]
    private GameObject torchCollider;

    // Start is called before the first frame update
    void Start()
    {
        activeState = State.Idle;

        player = GameObject.FindFirstObjectByType<PlayerMovement>().gameObject;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float sqrDistToPlayer = Vector2.SqrMagnitude(transform.position - player.transform.position);

        switch (activeState)
        {
            case State.Idle:
                if (sqrDistToPlayer < 25 && sqrDistToPlayer > 0.01)
                {
                    StateEnter(State.SeekPlayer);
                }
                break;
            case State.SeekPlayer:
                SeekPlayer();
                if (sqrDistToPlayer > 25) StateExit(State.Idle);
                if (sqrDistToPlayer < 0.5) StateEnter(State.Attack);
                break;
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

        spriteRenderer.color = Color.red;
        Invoke("ResetSpriteColor", 0.1f);
    }

    private void ResetSpriteColor() => spriteRenderer.color = Color.white;
}
