using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private float fill;
    // Start is called before the first frame update
    void Start()
    {
        healthBar = this.transform.GetChild(2).GetComponent<Image>();
    }

    public void UpdateHealthBar(float health, float maxHealth)
    {
        healthBar.type = Image.Type.Filled;
        healthBar.fillMethod = Image.FillMethod.Horizontal;
        healthBar.fillAmount = health / maxHealth;
    }
}
