using System;
using System.Collections.Generic;
using UnityEngine;

public class EngagementScript : MonoBehaviour
{
    public static EngagementScript current;

    private BackgroundMusicController.Location currentLocation;
    private List<string> engagedEnemies;

    private void Awake()
    {
        engagedEnemies = new List<string>();

        current = this;
    }

    private void Start()
    {
        current.onEngage += EngageUpdate;
        current.onDisengage += DisengageUpdate;
        BackgroundMusicController.current.onLocationSet += SetLocation;
    }

    private void OnDestroy()
    {
        current.onEngage -= EngageUpdate;
        current.onDisengage -= DisengageUpdate;
    }

    public event Action<string> onEngage;
    public void Engage(string name)
    {
        if (onEngage != null)
        {
            onEngage(name);
        }
    }

    public event Action<string> onDisengage;
    public void Disengage(string name)
    {
        if (onDisengage != null)
        {
            onDisengage(name);
        }
    }

    private void SetLocation(BackgroundMusicController.Location currentLocation)
    {
        if (currentLocation != BackgroundMusicController.Location.Combat)
            current.currentLocation = currentLocation;
    }

    private void EngageUpdate(string name)
    {
        if (current.engagedEnemies.Count == 0)
        {
            BackgroundMusicController.current.SetLocation(BackgroundMusicController.Location.Combat);
        }

        if (!current.engagedEnemies.Contains(name))
        {
            current.engagedEnemies.Add(name);
        }
    }

    private void DisengageUpdate(string name)
    {
        if (current.engagedEnemies.Contains(name))
        {
            current.engagedEnemies.Remove(name);

            if (current.engagedEnemies.Count == 0)
            {
                BackgroundMusicController.current.SetLocation(currentLocation);
            }
        }        
    }
}
