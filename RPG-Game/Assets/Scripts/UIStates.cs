using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIStates : MonoBehaviour
{
    [SerializeField]
    public PlayerControlManager playerControlManager;
    public GameObject knight;
    public Sprite knightsprite;
    public GameObject archer;
    public Sprite archersprite;

    public Image playerimage;
    public TMP_Text charactername;
    public TMP_Text HP;
    public TMP_Text Attack;
    public UISlot equipment;

    // Update is called once per frame
    void Update()
    {
        if (playerControlManager.currentPlayer == knight)
        {
            DisplayKnightStates();
        }
        else if (playerControlManager.currentPlayer == archer)
        {
            DisplayArcherStates();
        }
    }

    public void DisplayKnightStates() 
    {
        playerimage.sprite = knightsprite;
        charactername.text = "Knight";
        PlayerHealth playerhealth = knight.GetComponent<PlayerHealth>();
        PlayerAttack playerAttack = knight.GetComponent<PlayerAttack>();
        float currenthealth = playerhealth.currentHealth;
        float maxhealth = playerhealth.maxHealth;
        int attack = playerAttack.damage;
        HP.text = "HP: " + currenthealth + "/" + maxhealth;
        Attack.text = "Attack: " + attack;
        Slot chargeslot = knight.GetComponent<Slot>();
        Charge currentcharge = chargeslot.cur_charge;
        if (currentcharge != null) 
        {
            equipment.UpdateImage(currentcharge.sprite);
        }
        else equipment.Reset();

    }

    public void DisplayArcherStates() 
    {
        playerimage.sprite = archersprite;
        charactername.text = "Archer";
        PlayerHealth playerhealth = archer.GetComponent<PlayerHealth>();
        PlayerAttack playerAttack = archer.GetComponent<PlayerAttack>();
        float currenthealth = playerhealth.currentHealth;
        float maxhealth = playerhealth.maxHealth;
        int attack = playerAttack.damage;
        HP.text = "HP: " + currenthealth + "/" + maxhealth;
        Attack.text = "Attack: " + attack;
        Slot chargeslot = archer.GetComponent<Slot>();
        Charge currentcharge = chargeslot.cur_charge;
        if (currentcharge != null)
        {
            equipment.UpdateImage(currentcharge.sprite);
        }
        else equipment.Reset();
    }
}
