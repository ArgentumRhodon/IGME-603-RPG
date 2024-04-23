using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using static UnityEditor.Progress;
using static UnityEngine.InputSystem.Editor.InputActionCodeGenerator;

public class Slot : MonoBehaviour
{
    // Start is called before the first frame update
    public Charge cur_charge;
    public Collider2D item;
    public PowerButtonManager pbm;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (item != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //print("E pressed");
                Charge c = item.transform.GetComponent<Charge>();
                if (c != null)
                {
                    pickup(c);
                    //equip(c);
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
    public void Equip(Charge c)
    {
        if (GetComponentInParent<Inventory>().Charges.Find(x => x.c_type == c.c_type).equipped == false)
        {
            GetComponentInParent<Inventory>().Charges.Find(x => x.c_type == c.c_type).equipped = true;
            cur_charge = c;
        }
        else
        {
            print("This charge is in use");
        }
        GetComponent<Power>().powerup();
    }
    public void Unequip(Charge c)
    {
        GetComponentInParent<Inventory>().Charges.Find(x => x.c_type == c.c_type).equipped = false;
        cur_charge = null;
        GetComponent<Power>().powerup();
    }
    public void Equip(Potion p)
    {
        //Depends on the type
    }
    void pickup(Charge c)
    {
        c.equipped = false;
        //Charge newc = new Charge();
        //newc.equipped = c.equipped;
        //newc.c_type = c.c_type;
        //newc.sprite = c.sprite;
        //Charge_List.Add(c);
        //Charge_List.Sort((x, y) => x.c_type.CompareTo(y.c_type));
        GetComponentInParent<Inventory>().Charges.Add(c);
        GetComponentInParent<Inventory>().Charges.Sort((x, y) => x.c_type.CompareTo(y.c_type));
        pbm.UpdateBotton();
    }
    void pickup(Potion p)
    {
        //Potion newp = new Potion();
        //newp.sprite = p.sprite;
        //newp.p_type = p.p_type;
        GetComponentInParent<Inventory>().Potions.Add(p);
        GetComponentInParent<Inventory>().Potions.Sort((x, y) => x.p_type.CompareTo(y.p_type));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Pick Up UI enable here
        //print("Press E to pick up");
        item = collision;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //Pick Up UI disable here
        //print("Exit");
        item = null;
    }
    
    
}
