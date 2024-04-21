using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Charge_type
{
    Fire,
    Ice,
    Lightening
}

public class Charge : MonoBehaviour
{
    // Start is called before the first frame update
    public Charge_type c_type;
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
