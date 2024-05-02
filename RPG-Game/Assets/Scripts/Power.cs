using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public enum Player_type
{
    Knight,
    Archer
}
public class Power : MonoBehaviour
{
    // Start is called before the first frame update
    public float damage;
    public bool ischarge;
    public Charge_type c_type;
    public Player_type p_type;
    public Damage[] Damage;
    public PlayerAttack KnightAttack;
    public PlayerAttack ArcherAttack;
    public int attackPotion;
    void Start()
    {
        Damage = GetComponentsInChildren<Damage>();
        damage = 10;
        ischarge = false;
        powerup();
        attackPotion = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void powerup()
    {
        if(transform.gameObject.GetComponent<Slot>().cur_charge == null)
        {
            damage = 10;
            ischarge = false;
            if(KnightAttack != null)
            {
                KnightAttack.use1xcollider();
            }
            if(ArcherAttack != null)
            {
                ArcherAttack.Arrow3x = false;
            }
            for (int i = 0; i < Damage.Length; i++)
            {
                Damage[i].amount = damage;
            }
            return;
        }
        ischarge = true;
        c_type = transform.gameObject.GetComponent<Slot>().cur_charge.c_type;
        switch (c_type)
        {
            case Charge_type.Fire:
                if(p_type == Player_type.Knight)
                {
                    damage = 15 + attackPotion * 5;
                    KnightAttack.use1xcollider();
                }
                if(p_type == Player_type.Archer)
                {
                    ArcherAttack.Arrow3x = false;
                    damage = 30 + attackPotion * 5;
                }
                break;
            case Charge_type.Ice:
                if (p_type == Player_type.Knight)
                {
                    damage = 15 + attackPotion * 5;
                    KnightAttack.use1xcollider();
                }
                if (p_type == Player_type.Archer)
                {
                    ArcherAttack.Arrow3x = false;
                    damage = 10 + attackPotion * 5;
                }
                break;
            case Charge_type.Lightening:
                if (p_type == Player_type.Knight)
                {
                    damage = 10 + attackPotion * 5;
                    KnightAttack.use2xcollider();
                }
                if (p_type == Player_type.Archer)
                {
                    damage = 10 + attackPotion * 5;
                    ArcherAttack.Arrow3x = true;
                }
                break;
            default:
                break;
        }
        for(int i = 0; i < Damage.Length; i++)
        {
            Damage[i].amount = damage;
        }
    }
}

