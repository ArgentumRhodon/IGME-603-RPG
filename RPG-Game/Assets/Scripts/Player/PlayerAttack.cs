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

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnAttack()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        animator.SetTrigger("Attack");
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
