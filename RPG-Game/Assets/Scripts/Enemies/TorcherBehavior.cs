using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Search;
using UnityEngine;

enum State
{
    Idle,
    SeekPlayer,
    Attack
}

public class TorcherBehavior : MonoBehaviour
{
    private State activeState;

    private GameObject player;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

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
                break;
        }
    }

    private void SeekPlayer()
    {
        Vector2 movement = (player.transform.position - transform.position).normalized * Time.deltaTime;
        this.transform.position += (Vector3)movement;

        spriteRenderer.flipX = movement.x < 0;
    }

    private void StateEnter(State state)
    {
        switch (state)
        {
            case State.SeekPlayer:
                animator.SetBool("Run", true);
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
        Debug.Log("Hit!");
    }
}
