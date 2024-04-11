using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private float damage = 10f;
    Rigidbody2D rb;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindFirstObjectByType<PlayerMovement>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        float sqrDistToPlayer = Vector2.SqrMagnitude(transform.position - player.transform.position);
        Debug.Log(sqrDistToPlayer);

        if(sqrDistToPlayer < 25)
        {
            SeekPlayer();
            GetComponent<Animator>().SetBool("Run", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("Run", false);
        }
    }

    void SeekPlayer()
    {
        this.transform.position += (player.transform.position - transform.position).normalized * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerMovement>().TakeDamage(damage);
        }
    }
}
