using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerAttack : MonoBehaviour
{
    public int damage = 10;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private GameObject swordCollider;
    private GameObject enemies;

    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.FindFirstObjectByType<TorcherBehavior>().gameObject;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnAttack()
    {
        /*if (EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("Clicked");
            return;
        }*/
        //animator.SetTrigger("Attack");
        float dist = transform.position.y - enemies.transform.position.y;
        if (dist == 0 || Mathf.Abs(dist) > 1)
        {
            animator.SetTrigger("Attack");
        }
    }

    private void OnAttackUp()
    {
        float dist = transform.position.y - enemies.transform.position.y;
        if (dist < 0 && Mathf.Abs(dist) < 1)
        {
            animator.SetTrigger("AttackUp");
        }
    }

    private void OnAttackDown()
    {
        float dist = transform.position.y - enemies.transform.position.y;
        if (dist > 0 && Mathf.Abs(dist) < 1)  
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
