using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataTimer : MonoBehaviour
{
    public PlayerControlManager playerControlManager;
    public PlayTracker playTracker;

    public GameObject knight;
    public GameObject archer;

    private Power knightPower;
    private Power archerPower;

    public static PlayData playData;

    // Start is called before the first frame update
    void Start()
    {
        playTracker = GetComponent<PlayTracker>();

        playData = new PlayData(0, 0, 0, 0, 0, 0, 0, 0, 0);
        knightPower = knight.GetComponent<Power>();
        archerPower = archer.GetComponent<Power>();

        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    // Update is called once per frame
    void Update()
    {
        // Update knight data
        if(playerControlManager.currentPlayer == knight)
        {
            playData.knightTime += Time.deltaTime;

            // Charge time data
            if (knightPower.ischarge)
            {
                switch (knightPower.c_type)
                {
                    case Charge_type.Fire:
                        playData.knightFireTime += Time.deltaTime;
                        break;
                    case Charge_type.Ice:
                        playData.knightIceTime += Time.deltaTime;
                        break;
                    case Charge_type.Lightening:
                        playData.knightLightningTime += Time.deltaTime;
                        break;
                }
            }
        }
        // Update archer data
        else if(playerControlManager.currentPlayer == archer)
        {
            playData.archerTime += Time.deltaTime;

            // Charge time data
            if (archerPower.ischarge)
            {
                switch (archerPower.c_type)
                {
                    case Charge_type.Fire:
                        playData.archerFireTime += Time.deltaTime;
                        break;
                    case Charge_type.Ice:
                        playData.archerIceTime += Time.deltaTime;
                        break;
                    case Charge_type.Lightening:
                        playData.archerLightningTime += Time.deltaTime;
                        break;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("Saving data!");
            playTracker.AddPlayData(playData);
        }
    }

    private void OnSceneUnloaded(Scene current)
    {
        playTracker.AddPlayData(playData);
    }
}
