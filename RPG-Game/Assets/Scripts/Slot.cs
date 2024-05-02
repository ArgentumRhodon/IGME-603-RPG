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
    public PotionButtonManager potionButtonManager;
    public Sprite Sprite;
    public Sprite Sprite_Fire;
    public Sprite Sprite_Ice;
    public Sprite Sprite_Lightening;
    private PlayerHealth playerHealth;
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
                    item.transform.position = new Vector3(-1000, 400, 0);
                }
                //else print("not charge");
                Potion p = item.transform.GetComponent<Potion>();
                if (p != null)
                {
                    pickup(p);
                    item.transform.position = new Vector3(-1000, 400, 0);
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
            //if (c.c_type == Charge_type.Fire)
            //    GetComponent<SpriteRenderer>().sprite = Sprite_Fire;
            //if (c.c_type == Charge_type.Ice)
            //    GetComponent<SpriteRenderer>().sprite = Sprite_Ice;
            //if (c.c_type == Charge_type.Lightening)
            //    GetComponent<SpriteRenderer>().sprite = Sprite_Lightening;
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
        //GetComponent<SpriteRenderer>().sprite = Sprite;
        GetComponent<Power>().powerup();
    }
    public void Equip(Potion p)
    {
        if (GetComponentInParent<Inventory>().Potions.Find(x => x.p_type == p.p_type).Potion_equipped == false)
        {
            GetComponentInParent<Inventory>().Potions.Find(x => x.p_type == p.p_type).Potion_equipped = true;
            //Depends on the type
            if (p.p_type == Potion_type.Health)
            {
                PlayerControlManager.Instance.currentPlayer.GetComponent<PlayerHealth>().currentHealth += 20;
            }

            if (p.p_type == Potion_type.Attack)
            {
                StartCoroutine(IncreaseAttackValue());
            }

            if (p.p_type == Potion_type.BigHealth)
            {
                PlayerControlManager.Instance.currentPlayer.GetComponent<PlayerHealth>().currentHealth += 50;
            }
        }
        else
        {
            print("This charge is in use");
        }
    }

    public void Unequip(Potion p)
    {
        GetComponentInParent<Inventory>().Potions.Find(x => x.p_type == p.p_type).Potion_equipped = false;
    }

    IEnumerator IncreaseAttackValue()
    {
        float startTime = Time.time;
        while(Time.time - startTime < 60)
        {
            PlayerControlManager.Instance.currentPlayer.GetComponent<PlayerAttack>().damage = 15;
            yield return new WaitForSeconds(1);
        }
        PlayerControlManager.Instance.currentPlayer.GetComponent<PlayerAttack>().damage = 10;
        Debug.Log("One minute has passed!");
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
        p.Potion_equipped = false;
        //Potion newp = new Potion();
        //newp.sprite = p.sprite;
        //newp.p_type = p.p_type;
        GetComponentInParent<Inventory>().Potions.Add(p);
        GetComponentInParent<Inventory>().Potions.Sort((x, y) => x.p_type.CompareTo(y.p_type));
        potionButtonManager.UpdateButton();
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
