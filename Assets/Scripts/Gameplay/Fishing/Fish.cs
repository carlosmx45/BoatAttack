using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish
{
    public string Name { get; set; }
    public float CatchProbability { get; set; }
    public int Reward { get; set; }
    public Sprite FishSprite { get; set; }

    public Fish(string name, float catchProbability, int reward, Sprite fishSprite)
    {
        Name = name;
        CatchProbability = catchProbability;
        Reward = reward;
        FishSprite = fishSprite;
    }
}