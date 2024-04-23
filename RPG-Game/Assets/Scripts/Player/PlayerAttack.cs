using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerAttack : MonoBehaviour
{
    public int damage = 10;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private GameObject swordCollider;
    private GameObject closestEnemy;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnAttack()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float mx = mousePosition.x - transform.position.x;
        float my = mousePosition.y - transform.position.y;

        if(-mx <= my && my <= mx) 
        {
            spriteRenderer.flipX = false;
            animator.SetTrigger("Attack");
        }
        else if(mx < my && my < -mx)
        {
            spriteRenderer.flipX = true;
            animator.SetTrigger("Attack");
        }
        else if(my > -mx && my > mx)
        {
            animator.SetTrigger("AttackUp");
        }
        else
        {
            animator.SetTrigger("AttackDown");
        }
    }

    public void OnAim()
    {
        animator.SetTrigger("Aim");
    }

    public void EnableSwordCollider()
    {
        swordCollider.SetActive(true);

        if (spriteRenderer.flipX)
        {
            swordCollider.transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            swordCollider.transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }
    }

    public void DisableSwordCollider()
    {
        swordCollider.SetActive(false);
    }
}
