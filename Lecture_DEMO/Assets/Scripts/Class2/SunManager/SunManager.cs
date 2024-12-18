using System;
using UnityEngine;

public class SunManager : MonoBehaviour,ISunManager
{
    private int SunNum{get;set;}
    public void SunReduce(int sunCost)
    {
        SunNum -= sunCost;
    }

    public void SunIncrease()
    {
        SunNum += 25;
    }

    public int GetSunValue()
    {
        int sunValue = SunNum;
        return sunValue;
    }
    
    private void Start()
    {
        SunNum = 100;
    }
}