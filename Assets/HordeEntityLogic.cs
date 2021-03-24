﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HordeEntityLogic : MonoBehaviour 
{
    public float currentValue;
    HordeLogic hordeLogic;
    // Start is called before the first frame update
    void Start()
    {
        hordeLogic=FindObjectOfType<HordeLogic>();
        hordeLogic.enemies.Add(gameObject);
        HealthEventSystem.current.onDeath += KillEntity;
     
    }

   
    public void KillEntity(string name)
    {
        if (name == gameObject.name)
        {
            hordeLogic.enemies.Remove(gameObject);
        }
    }
    public void OnDestroy()
    {
        HealthEventSystem.current.onDeath -= KillEntity;
    }
}