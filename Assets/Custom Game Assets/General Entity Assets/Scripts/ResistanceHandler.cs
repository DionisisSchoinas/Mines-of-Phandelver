using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResistanceHandler : MonoBehaviour
{
    //[HideInInspector]
    public List<ElementTypes.Type> damageResistances;
    //[HideInInspector]
    public List<ElementTypes.Type> damageImmunities;

    private Coroutine resistanceTimer;
    private bool countingResistanceDuration;

    private void Awake()
    {
        damageResistances = new List<ElementTypes.Type>();
        damageImmunities = new List<ElementTypes.Type>();
    }

    private void Start()
    {
        HealthEventSystem.current.onResistanceApply += ApplyResistance;
    }

    private void OnDestroy()
    {
        HealthEventSystem.current.onResistanceApply -= ApplyResistance;
    }

    //-------------- Resistance Management --------------
    public void ApplyResistance(int id, List<SkinnedMeshRenderer> meshes, Material newMaterial, ElementTypes.Type resistance, float duration)
    {
        if (gameObject.GetInstanceID() != id)
            return;

        RemoveResistance(meshes);
        AddResistance(meshes, newMaterial, resistance, duration);
    }

    private void AddResistance(List<SkinnedMeshRenderer> meshes, Material newMaterial, ElementTypes.Type resistance, float duration)
    {
        foreach (SkinnedMeshRenderer mesh in meshes)
        {
            Material[] mats = mesh.materials;

            Material newMat = newMaterial;

            newMat.SetTexture("_MainTexture", mats[0].GetTexture("_MainTexture"));
            newMat.SetTexture("_NormalMap", mats[0].GetTexture("_NormalMap"));
            newMat.SetInt("_HasExtra", 1);

            mats[0] = newMat;

            mesh.materials = mats; // Adds the resistance material to the mesh
        }

        damageResistances.Add(resistance); // Add resistance to list

        Sprite icon;
        switch (resistance)
        {
            case ElementTypes.Type.Cold_Ice:
                icon = ResourceManager.UI.SkillIcons.Resistance.Ice;
                break;
            case ElementTypes.Type.Physical_Earth:
                icon = ResourceManager.UI.SkillIcons.Resistance.Earth;
                break;
            case ElementTypes.Type.Lightning:
                icon = ResourceManager.UI.SkillIcons.Resistance.Lightning;
                break;
            default:
                icon = ResourceManager.UI.SkillIcons.Resistance.Fire;
                break;
        }

        UIEventSystem.current.ApplyResistance(icon, duration);
        HealthEventSystem.current.UpdateResistance(gameObject.GetInstanceID(), damageResistances);

        resistanceTimer = StartCoroutine(StartDuration(meshes, duration));
    }

    private void RemoveResistance(List<SkinnedMeshRenderer> meshes)
    {
        if (!countingResistanceDuration)
            return;

        StopCoroutine(resistanceTimer); //Stops coroutine counting duration

        foreach (SkinnedMeshRenderer mesh in meshes)
        {
            Material[] mats = mesh.materials;

            Material newMat = mats[0];

            newMat.SetInt("_HasExtra", 0);

            mats[0] = newMat;

            mesh.materials = mats; // Adds the resistance material to the mesh
        }

        damageResistances.Clear(); // Empty resistance list ( works since we only have 1 way to add resistances )

        UIEventSystem.current.RemoveResistance();
        HealthEventSystem.current.UpdateResistance(gameObject.GetInstanceID(), damageResistances);
    }

    private IEnumerator StartDuration(List<SkinnedMeshRenderer> meshes, float duration)
    {
        countingResistanceDuration = true;
        yield return new WaitForSeconds(duration);
        RemoveResistance(meshes);
        countingResistanceDuration = false;
    }
}