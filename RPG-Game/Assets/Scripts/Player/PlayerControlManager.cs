using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerControlManager : MonoBehaviour
{
    public GameObject[] knights;
    public GameObject[] archers;

    public GameObject knightbasic;
    public GameObject archerbasic;
    private Transform playerTransform;
    private int selectedCharacter = 0;

    public GameObject currentPlayer;
    public GameObject newPlayer;
    private PlayerMovement playerMovement;
    // Start is called before the first frame update
    private static PlayerControlManager _instance;
    public static PlayerControlManager Instance { get { return _instance; } }

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    void Start()
    {
        archerbasic.SetActive(false);
        currentPlayer = knightbasic;
        playerMovement = currentPlayer.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerMovement != null)
        {
            playerTransform = knightbasic.activeSelf ? knightbasic.transform : archerbasic.transform;
        }
    }

    public void OnSwitch()
    {
        if (knightbasic.activeInHierarchy == true)
        {
            playerMovement.movementVector = Vector2.zero;
            currentPlayer = archerbasic;
            archerbasic.transform.position = playerTransform.position;
            archerbasic.SetActive(true);
            knightbasic.SetActive(false);
        }
        else
        {
            playerMovement.movementVector = Vector2.zero;
            currentPlayer = knightbasic;
            knightbasic.transform.position = playerTransform.position;
            knightbasic.SetActive(true);
            archerbasic.SetActive(false);
        }
    }

    public void OnKnightSwitchType()
    {
        if (selectedCharacter == 3)
        {
            selectedCharacter = 0;
        }
        else
        {
            selectedCharacter++;
        }
        for (int i = 0; i < 4; i++)
        {
            if (i != selectedCharacter)
            {
                knights[i].SetActive(false);
            }
            else
            {
                knights[selectedCharacter].SetActive(true);
            }
        }
    }

    public void OnArcherSwitchType()
    {
        if (selectedCharacter == 3)
        {
            selectedCharacter = 0;
        }
        else
        {
            selectedCharacter++;
        }
        for (int i = 0; i < 4; i++)
        {
            if (i != selectedCharacter)
            {
                archers[i].SetActive(false);
            }
            else
            {
                archers[selectedCharacter].SetActive(true);
            }
        }
    }
}
