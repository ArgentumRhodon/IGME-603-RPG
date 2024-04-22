using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlManager : MonoBehaviour
{
    public GameObject knight;
    public GameObject archer;
    private Transform playerTransform;
    public GameObject currentPlayer;
    // Start is called before the first frame update
    void Start()
    {
        archer.SetActive(false);
        currentPlayer = knight;
    }

    // Update is called once per frame
    void Update()
    {
        playerTransform = knight.activeSelf ? knight.transform : archer.transform;
    }

    public void OnSwitch()
    {
        if (knight.activeInHierarchy == true)
        {
            currentPlayer = archer;
            archer.transform.position = playerTransform.position;
            archer.SetActive(!archer.activeSelf);
            knight.SetActive(!knight.activeSelf);
        }
        else
        {
            currentPlayer = knight;
            knight.transform.position = playerTransform.position;
            knight.SetActive(!knight.activeSelf);
            archer.transform.position = playerTransform.position;
        }
    }
}
