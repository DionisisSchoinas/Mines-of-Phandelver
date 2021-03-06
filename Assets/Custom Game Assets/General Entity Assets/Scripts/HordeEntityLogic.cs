using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HordeEntityLogic : MonoBehaviour 
{
    private HordeLogic hordeLogic;

    void Start()
    {
        hordeLogic = GetComponentInParent<HordeLogic>();
        hordeLogic.enemies.Add(gameObject);
        HealthEventSystem.current.onDeath += KillEntity;
    }   

    public void OnDestroy()
    {
        HealthEventSystem.current.onDeath -= KillEntity;
    }

    public void KillEntity(int id)
    {
        if (id == gameObject.GetInstanceID())
        {
            hordeLogic.enemies.Remove(gameObject);
        }
    }
}
