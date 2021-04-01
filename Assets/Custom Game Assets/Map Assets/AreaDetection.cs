using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDetection : MonoBehaviour
{
    [System.Serializable]
    public class AreaSettings
    {
        public string name;
        public BackgroundMusicController.Location location;
        public Light mainLight;
        public Light backlight;

        public AreaSettings Copy()
        {
            AreaSettings copy = new AreaSettings();
            copy.name = name;
            copy.location = location;
            copy.mainLight = mainLight;
            copy.backlight = backlight;
            return copy;
        }
    }

    public AreaSettings areaSettings;
    private SwapAreasScript parent;

    private void Awake()
    {
        parent = gameObject.GetComponentInParent<SwapAreasScript>();
    }

    private void OnTriggerEnter(Collider other)
    {
        parent.TriggerEnter(areaSettings);
    }
}
