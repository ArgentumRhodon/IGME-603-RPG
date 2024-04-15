using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitDetection : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"{collision.gameObject.name} collided with {gameObject.name}");

        spriteRenderer.color = Color.red;
        Invoke("ResetSpriteColor", 0.1f);
    }

    private void ResetSpriteColor()
    {
        spriteRenderer.color = Color.white;
    }
}
