using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapAreasScript : MonoBehaviour
{
    private AreaDetection.AreaSettings currentArea;

    private void Awake()
    {
        currentArea = new AreaDetection.AreaSettings();
    }

    private void Start()
    {
        SpawnAreaSetter.current.onLocationSet += LocationChanged;
    }

    private void OnDestroy()
    {
        SpawnAreaSetter.current.onLocationSet -= LocationChanged;
    }

    private void LocationChanged(AreaDetection.AreaSettings location)
    {
        currentArea = location.Copy();
    }

    public void TriggerEnter(AreaDetection.AreaSettings areaSettings)
    {
        if (areaSettings.location == currentArea.location)
            return;

        areaSettings.mainLight.enabled = true;
        areaSettings.backlight.enabled = false;

        if (currentArea.mainLight != null)
            currentArea.mainLight.enabled = false;
        if (currentArea.backlight != null)
            currentArea.backlight.enabled = false;

        BackgroundMusicController.current.SetLocation(areaSettings.location);

        currentArea = areaSettings.Copy();
    }
}
