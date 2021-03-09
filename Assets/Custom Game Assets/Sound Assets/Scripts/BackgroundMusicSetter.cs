using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BackgroundMusicSetter : MonoBehaviour
{
    public BackgroundMusicController.Location location;

    private BackgroundMusicController.Location currentLocation;

    private void Start()
    {
        BackgroundMusicController.current.onLocationSet += SetLocation;
    }

    private void OnDestroy()
    {
        BackgroundMusicController.current.onLocationSet -= SetLocation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && currentLocation != BackgroundMusicController.Location.Combat)
        {
            BackgroundMusicController.current.SetLocation(location);
        }
    }

    private void SetLocation(BackgroundMusicController.Location currentLocation)
    {
        this.currentLocation = currentLocation;
    }
}
