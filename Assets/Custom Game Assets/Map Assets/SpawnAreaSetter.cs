using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAreaSetter : MonoBehaviour
{
    public static SpawnAreaSetter current;

    public AreaDetection.AreaSettings firstArea;

    private void Awake()
    {
        current = this;
    }

    private void Start()
    {
        StartCoroutine(SetArea());
    }

    private IEnumerator SetArea()
    {
        yield return new WaitForEndOfFrame();
        SetLocation(firstArea);
    }

    public event Action<AreaDetection.AreaSettings> onLocationSet;
    public void SetLocation(AreaDetection.AreaSettings location)
    {
        if (onLocationSet != null)
        {
            onLocationSet(location);
        }
    }
}
