using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Start is called before the first frame update
    List<Weapon> Weapons;
    List<Charge> Charges;
    List<Potion> Potions;
    void Start()
    {
        Weapons = new List<Weapon>();
        Charges = new List<Charge>();
        Potions = new List<Potion>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void pickup(Weapon w)
    {
        w.equipped = false;
        Weapons.Add(w);
        Weapons.Sort((x, y) => x.w_type.CompareTo(y.w_type));
    }
    void pickup(Charge c)
    {
        c.equipped = false;
        Charges.Add(c);
        Charges.Sort((x, y) => x.c_type.CompareTo(y.c_type));
    }
    void pickup(Potion p)
    {
        Potions.Add(p);
        Potions.Sort((x, y) => x.p_type.CompareTo(y.p_type));
    }
    void equip(Weapon w)
    {
        Weapons.Find(x => x.w_type == w.w_type).equipped = true;
        //switch slot
    }
    void equip(Charge c)
    {
        Charges.Find(x => x.c_type == c.c_type).equipped = true;
        //switch slot
    }
    void equip(Potion p)
    {

    }
}
