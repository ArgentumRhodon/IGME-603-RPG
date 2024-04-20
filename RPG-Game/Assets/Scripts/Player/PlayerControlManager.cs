using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlManager : MonoBehaviour
{
    public GameObject knight;
    public GameObject archer;
    // Start is called before the first frame update
    void Start()
    {
        archer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSwitch()
    {
        knight.SetActive(!knight.activeSelf);
        archer.SetActive(!archer.activeSelf);
    }
}
