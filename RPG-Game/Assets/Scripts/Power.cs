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
    void Start()
    {
        damage = transform.gameObject.GetComponent<Damage>().amount;
        ischarge = false;
        powerup();
        print("Ice = " + (int)Charge_type.Ice);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void powerup()
    {
        if(transform.gameObject.GetComponentInParent<Slot>().cur_charge == null)
        {
            damage = 10;
            ischarge = false;
            return;
        }
        ischarge = true;
        c_type = transform.gameObject.GetComponentInParent<Slot>().cur_charge.c_type;
        switch (c_type)
        {
            case Charge_type.Fire:
                if(p_type == Player_type.Knight)
                {
                    damage = 15;
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
                }
                if (p_type == Player_type.Archer)
                {
                    damage = 30;
                }
                break;
        }
    }
}

