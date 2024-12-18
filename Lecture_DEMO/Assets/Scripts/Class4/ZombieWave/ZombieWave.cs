using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ZombieWave
{
    [HideInInspector] public bool IsHappend = false;
    public float TimePoint;
    public int ZombieCount;

    public ZombieWave(float timePoint, int zombieCount)
    {
        TimePoint = timePoint;
        ZombieCount = zombieCount;
    }
}