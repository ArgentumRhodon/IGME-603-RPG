using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Potion_type
{
    Health,
    Attack,
    BigHealth
}

public class Potion : MonoBehaviour
{
    // Start is called before the first frame update
    public Potion_type p_type;
    public Sprite sprite;
    public bool Potion_equipped;
    void Start()
    {
        Potion_equipped = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
