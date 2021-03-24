using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class KillGoal : Goal
{
    public HordeLogic horde;
    public KillGoal()
    {
        type = "Elimination";
    }
    public void addKill()
    {
       
    }
}
