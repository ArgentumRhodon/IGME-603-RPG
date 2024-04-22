using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlManager : MonoBehaviour
{
    public GameObject knight;
    public GameObject archer;
    private Transform playerTransform;
    public GameObject currentPlayer;
    private PlayerMovement playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        archer.SetActive(false);
        currentPlayer = knight;
        playerMovement = currentPlayer.GetComponent<PlayerMovement>();
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
            playerMovement.movementVector = Vector2.zero;
            currentPlayer = archer;
            archer.transform.position = playerTransform.position;
            archer.SetActive(true);
            knight.SetActive(false);
        }
        else
        {
            playerMovement.movementVector = Vector2.zero;
            currentPlayer = knight;
            knight.transform.position = playerTransform.position;
            knight.SetActive(true);
            archer.SetActive(false);
        }
    }
}
