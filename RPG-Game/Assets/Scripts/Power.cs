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
    void Start()
    {
        Damage = GetComponentsInChildren<Damage>();
        damage = 10;
        ischarge = false;
        powerup();
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
                    damage = 15;
                    KnightAttack.use1xcollider();
                }
                if(p_type == Player_type.Archer)
                {
                    damage = 50;
                }
                break;
            case Charge_type.Ice:
                if (p_type == Player_type.Knight)
                {
                    damage = 10;
                    KnightAttack.use1xcollider();
                }
                if (p_type == Player_type.Archer)
                {
                    damage = 10;
                }
                break;
            case Charge_type.Lightening:
                if (p_type == Player_type.Knight)
                {
                    damage = 10;
                    KnightAttack.use2xcollider();
                }
                if (p_type == Player_type.Archer)
                {
                    damage = 30;
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

