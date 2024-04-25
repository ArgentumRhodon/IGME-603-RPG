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
    [SerializeField]
    private GameObject upSwordCollider;
    [SerializeField]
    private GameObject downSwordCollider;
    private GameObject closestEnemy;
    
    public GameObject swordCollider1x; 
    public GameObject upSwordCollider1x;    
    public GameObject downSwordCollider1x;

    public GameObject swordCollider2x;
    public GameObject upSwordCollider2x;
    public GameObject downSwordCollider2x;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// References the mouse position relative to the player to determine direction of attack
    /// Mouse position is considered according to the quadrants formed by y=x and y=-x and the
    /// directions they represent
    /// </summary>
    public void use2xcollider()
    {
        swordCollider = swordCollider2x;
        upSwordCollider = upSwordCollider2x;
        downSwordCollider = downSwordCollider2x;
        print("2x");
    }
    public void use1xcollider()
    {
        swordCollider = swordCollider1x;
        upSwordCollider = upSwordCollider1x;
        downSwordCollider = downSwordCollider1x;
        print("1x");
    }
    private void OnAttack()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float mx = mousePosition.x - transform.position.x;
        float my = mousePosition.y - transform.position.y - 1;

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

    private void AdjustColliderOrientation(GameObject collider)
    {
        if (spriteRenderer.flipX)
        {
            collider.transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            collider.transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }
    }

    // Attack Left/Right
    public void EnableSwordCollider()
    {
        swordCollider.SetActive(true);

        AdjustColliderOrientation(swordCollider);
    }
    public void DisableSwordCollider()
    {
        swordCollider.SetActive(false);
    }

    // Attack Up
    public void EnableUpSwordCollider()
    {
        upSwordCollider.SetActive(true);
        AdjustColliderOrientation(upSwordCollider);
    }

    public void DisableUpSwordCollider()
    {
        upSwordCollider.SetActive(false);
    }

    // Attack Down
    public void EnableDownSwordCollider()
    {
        downSwordCollider.SetActive(true);
        AdjustColliderOrientation(downSwordCollider);
    }
    public void DisableDownSwordCollider()
    {
        downSwordCollider.SetActive(false);
    }
}
