using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Start is called before the first frame update
    //public List<Weapon> Weapons;
    public List<Charge> Charges;
    public List<Potion> Potions;
    public Collider2D item;
    void Start()
    {
        //Weapons = new List<Weapon>();
        Charges = new List<Charge>();
        Potions = new List<Potion>();
    }

    // Update is called once per frame
    void Update()
    {
        if (item != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                print("E pressed");
                Charge c = item.transform.GetComponent<Charge>();
                if (c != null)
                {
                    pickup(c);
                    item.transform.position = new Vector3(-10, 4, 0);
                }
                //else print("not charge");
                Potion p = item.transform.GetComponent<Potion>();
                if (p != null)
                {
                    pickup(p);
                    item.transform.position = new Vector3(-10, 4, 0);
                }
                //else print("not potion");
            }
        }
    }
    //void pickup(Weapon w)
    //{
    //    w.equipped = false;
    //    Weapons.Add(w);
    //    Weapons.Sort((x, y) => x.w_type.CompareTo(y.w_type));
    //}
    void pickup(Charge c)
    {
        c.equipped = false;
        //Charge newc = new Charge();
        //newc.equipped = c.equipped;
        //newc.c_type = c.c_type;
        //newc.sprite = c.sprite;
        Charges.Add(c);
        Charges.Sort((x, y) => x.c_type.CompareTo(y.c_type));
    }
    void pickup(Potion p)
    {
        //Potion newp = new Potion();
        //newp.sprite = p.sprite;
        //newp.p_type = p.p_type;
        Potions.Add(p);
        Potions.Sort((x, y) => x.p_type.CompareTo(y.p_type));
    }
    //void equip(Weapon w)
    //{
    //    Weapons.Find(x => x.w_type == w.w_type).equipped = true;
    //    //switch slot
    //}
    void equip(Charge c)
    {
        Charges.Find(x => x.c_type == c.c_type).equipped = true;
        //switch slot
    }
    void equip(Potion p)
    {
        //Depends on the type
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Pick Up UI enable here
        print("Press E to pick up");
        item = collision;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        //print("IN STAY");
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //Pick Up UI disable here
        print("Exit");
        item = null;
    }
    
}
