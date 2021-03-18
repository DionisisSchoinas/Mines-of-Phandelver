using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneBall : SpellTypeBall
{
    public override string skillName => "Stone Ball";

    private GameObject path;

    private void Start()
    {
        path = GetComponentInChildren<ParticleSystem>().gameObject;
        path.SetActive(false);
        MovePath();
        path.SetActive(true);

        AudioSource audioSource1 = gameObject.AddComponent<AudioSource>();
        audioSource1 = ResourceManager.Audio.AudioSources.LoadAudioSource("Sound Effects", audioSource1, ResourceManager.Audio.AudioSources.Range.Short);
        audioSource1.loop = true;
        audioSource1.clip = ResourceManager.Audio.Spells.Earth.BallTrail;
        audioSource1.Play();
    }

    private new void FixedUpdate()
    {
        base.FixedUpdate();
        MovePath();
        rb.AddForce(transform.forward * speed * Time.deltaTime, ForceMode.VelocityChange);
    }

    private void MovePath()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            path.transform.position = hit.point;
        }
    }

    public override ParticleSystem GetSource()
    {
        return ResourceManager.Sources.Spells.Earth;
    }
}
