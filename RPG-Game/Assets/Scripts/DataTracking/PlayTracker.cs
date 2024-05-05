using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayTracker : MonoBehaviour
{
    List<PlayData> plays = new List<PlayData>();

    public void AddPlayData(PlayData data)
    {
        UpdatePlayData();
        plays.Add(data);
        SaveData.SaveToJSON<PlayData>(plays, "PlayData.json");
    }

    public void UpdatePlayData()
    {
        plays = SaveData.ReadFromJSON<PlayData>("PlayData.json");
    }
}
