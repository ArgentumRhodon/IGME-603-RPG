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
    private SpawnArrow spawnArrow;
    [SerializeField]
    private GameObject Arrow;
    [SerializeField]
    private GameObject SpawnRight;
    [SerializeField]
    private GameObject SpawnLeft;
    [SerializeField]
    private GameObject SpawnUp;
    [SerializeField]
    private GameObject SpawnDown;
    [SerializeField]
    private Transform SpawnPointRight;
    [SerializeField]
    private Transform SpawnPointLeft;
    [SerializeField]
    private Transform SpawnPointUp;
    [SerializeField]
    private Transform SpawnPointDown;

    private float ArrowSpeed = 5f;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spawnArrow = GetComponent<SpawnArrow>();
    }

    /// <summary>
    /// References the mouse position relative to the player to determine direction of attack
    /// Mouse position is considered according to the quadrants formed by y=x and y=-x and the
    /// directions they represent
    /// </summary>
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
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float mx = mousePosition.x - transform.position.x;
        float my = mousePosition.y - transform.position.y - 1;

        if (-mx <= my && my <= mx)
        {
            spriteRenderer.flipX = false;
            animator.SetTrigger("Aim");
        }
        else if (mx < my && my < -mx)
        {
            spriteRenderer.flipX = true;

            animator.SetTrigger("Aim");
        }
        else if (my > -mx && my > mx)
        {
            animator.SetTrigger("AimUp");
        }
        else
        {
            animator.SetTrigger("AimDown");
        }
        //animator.SetTrigger("Aim");
    }

    public void CastRight()
    {
        Instantiate(Arrow, SpawnPointRight.position, SpawnPointRight.rotation);
        Arrow.GetComponent<Rigidbody2D>().velocity = transform.right * ArrowSpeed;
    }

    public void CastLeft()
    {
        Instantiate(Arrow, SpawnPointLeft.position, SpawnPointLeft.rotation);
        Arrow.GetComponent<Rigidbody2D>().velocity = transform.right * ArrowSpeed;
    }

    public void CastUp()
    {
        Instantiate(Arrow, SpawnPointUp.position, SpawnPointUp.rotation);
        Arrow.GetComponent<Rigidbody2D>().velocity = transform.right * ArrowSpeed;
    }

    public void CastDown()
    {
        Instantiate(Arrow, SpawnPointDown.position, SpawnPointDown.rotation);
        Arrow.GetComponent<Rigidbody2D>().velocity = transform.right * ArrowSpeed;
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

    public void EnableSpawnPointRight()
    {
        if (spriteRenderer.flipX == false) 
        {
            SpawnRight.SetActive(true);
            CastRight();
        }
        else
        {
            SpawnLeft.SetActive(true);
            CastLeft();
        }
    }

    public void DisableSpawnPointRight()
    {
        SpawnRight.SetActive(false);
        SpawnLeft.SetActive(false);
    }

    public void EnableSpawnPointUp()
    {
        SpawnUp.SetActive(true);
        CastUp();
    }

    public void DisableSpawnPointUp()
    {
        SpawnUp.SetActive(false);
    }

    public void EnableSpawnPointDown()
    {
        SpawnDown.SetActive(true);
        CastDown();
    }

    public void DisableSpawnPointDown()
    {
        SpawnDown.SetActive(false);
    }
}
