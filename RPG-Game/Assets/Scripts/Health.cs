using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private Animator animator;

    public float maxHealth;
    public float currentHealth;

    private void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    public void IncreaseHealth(float value)
    {
        currentHealth += value;
    }

    public void ReduceHealth(float value)
    {
        currentHealth -= value;
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
