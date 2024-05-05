using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayData
{
    public float archerTime = 0.0f;
    public float knightTime = 0.0f;
    public int swaps = 0;

    public float archerFireTime = 0.0f;
    public float archerIceTime = 0.0f;
    public float archerLightningTime = 0.0f;

    public float knightFireTime = 0.0f;
    public float knightIceTime = 0.0f;
    public float knightLightningTime = 0.0f;

    public PlayData(float archerTime, float knightTime, int swaps, float aft, float ait, float alt, float kft, float kit, float klt)
    {
        this.archerTime = archerTime;
        this.knightTime = knightTime;
        this.swaps = swaps;

        archerFireTime = aft;
        archerIceTime = ait;
        archerLightningTime = alt;

        knightFireTime = kft;
        knightIceTime = kit;
        knightLightningTime = klt;
    }
}
