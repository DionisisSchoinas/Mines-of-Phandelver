using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResistanceHandler : MonoBehaviour
{
    //[HideInInspector]
    public List<int> damageResistances;
    //[HideInInspector]
    public List<int> damageImmunities;

    private Coroutine resistanceTimer;
    private bool countingResistanceDuration;

    private void Awake()
    {
        damageResistances = new List<int>();
        damageImmunities = new List<int>();
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
    public void ApplyResistance(string name, List<SkinnedMeshRenderer> meshes, Material newMaterial, int resistance, float duration)
    {
        if (gameObject.name != name)
            return;

        RemoveResistance(meshes);
        AddResistance(meshes, newMaterial, resistance, duration);
    }

    private void AddResistance(List<SkinnedMeshRenderer> meshes, Material newMaterial, int resistance, float duration)
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

        UIEventSystem.current.ApplyResistance(DamageTypesManager.Types[resistance] + " Resistance", duration);
        HealthEventSystem.current.UpdateResistance(gameObject.name, damageResistances);

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
        HealthEventSystem.current.UpdateResistance(gameObject.name, damageResistances);
    }

    private IEnumerator StartDuration(List<SkinnedMeshRenderer> meshes, float duration)
    {
        countingResistanceDuration = true;
        yield return new WaitForSeconds(duration);
        RemoveResistance(meshes);
        countingResistanceDuration = false;
    }
}