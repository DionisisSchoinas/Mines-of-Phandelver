using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class StepDetection : MonoBehaviour
{
    public enum Direction
    {
        Up,
        Down,
        Right,
        Left,
        Forward,
        Backward,
    }

    public LayerMask groundLayer;
    public float distanceFromGround = 0.5f;
    public Direction jointToGroundDirection;
    public List<Transform> groundContactPoints;

    private List<bool> grounded;
    private List<AudioSource> audioSources;

    private void Start()
    {
        if (groundContactPoints == null || groundContactPoints.Count == 0)
            groundContactPoints = new List<Transform>();

        grounded = new List<bool>();
        foreach (Transform t in groundContactPoints)
            grounded.Add(false);

        audioSources = new List<AudioSource>();
        for (int i=0; i < groundContactPoints.Count; i++)
        {
            audioSources.Add(groundContactPoints[i].gameObject.AddComponent<AudioSource>());
            audioSources[i] = ResourceManager.Audio.AudioSources.LoadAudioSource("Sound Effects", audioSources[i], ResourceManager.Audio.AudioSources.Range.Mid);
            audioSources[i].clip = ResourceManager.Audio.Footsteps.WalkGrass;
        }
    }

    private void FixedUpdate()
    {
        for (int i=0; i<groundContactPoints.Count; i++)
        {
            //bool touch = Physics.CheckSphere(groundContactPoints[i].position, distanceFromGround, groundLayer);


            RaycastHit hit1;
            RaycastHit hit2;
            bool touch1 = Physics.Raycast(groundContactPoints[i].position, Vector3.down, out hit1, distanceFromGround, groundLayer);
            bool touch2 = Physics.Raycast(groundContactPoints[i].position, FindDirection(groundContactPoints[i]), out hit2, distanceFromGround, groundLayer);

            if (touch1 && touch2 && !grounded[i]) // Collides with ground and is not already grounded
            {
                audioSources[i].Play();
                grounded[i] = true;
            }
            else if ( !(touch1 && touch2) && grounded[i] ) // Doesn't collide with ground and is grounded 
            {
                grounded[i] = false;
            }
        }
    }

    private Vector3 FindDirection(Transform transform)
    {
        switch (jointToGroundDirection)
        {
            case Direction.Down:
                return -transform.up;
            case Direction.Right:
                return transform.right;
            case Direction.Left:
                return -transform.right;
            case Direction.Forward:
                return transform.forward;
            case Direction.Backward:
                return -transform.forward;
            default:
                return transform.up;
        }
    }

    private void OnDrawGizmos()
    {
        if (groundContactPoints == null)
            return;

        for (int i=0; i < groundContactPoints.Count; i++)
        {
            if (groundContactPoints[i] != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(groundContactPoints[i].position, groundContactPoints[i].position + Vector3.down * distanceFromGround);

                Gizmos.color = Color.blue;
                Gizmos.DrawLine(groundContactPoints[i].position, groundContactPoints[i].position + (FindDirection(groundContactPoints[i]) * distanceFromGround));

                try
                {
                    if (grounded[i])
                        Gizmos.color = Color.red;
                    else
                        Gizmos.color = Color.white;
                    Gizmos.DrawSphere(groundContactPoints[i].position + Vector3.up, 0.5f);
                }
                catch { }
            }
        }
    }
}
