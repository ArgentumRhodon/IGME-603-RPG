using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private Animator animator;
    [SerializeField]
    private GameObject diedPanel;
    [SerializeField] private HealthBar healthBar;

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
        Debug.Log("Current: " + currentHealth);
        currentHealth += value;
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
    }

    public void ReduceHealth(float value)
    {
        currentHealth -= value;
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
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
        //Destroy(gameObject);
    }
}
