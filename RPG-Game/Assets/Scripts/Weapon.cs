using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Weapon_type
{
    Axe,
    Hammer
}

public class Weapon : MonoBehaviour
{
    // Start is called before the first frame update
    public Weapon_type w_type;
    public Sprite sprite;
    public bool equipped;
    void Start()
    {
        equipped = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
