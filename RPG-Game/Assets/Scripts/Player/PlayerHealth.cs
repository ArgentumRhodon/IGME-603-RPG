using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private Animator animator;
    [SerializeField]
    private GameObject diedPanel;

    public float maxHealth;
    public float currentHealth;
    private bool dying = false;

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

    private void Update()
    {
        if(currentHealth <= 0 && !dying)
        {
            dying = true;
            animator.SetTrigger("Death");
        }
    }

    private void Die()
    {
        diedPanel.SetActive(true);
        Destroy(gameObject);
    }
}
