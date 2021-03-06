using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi_V2 : MonoBehaviour
{
    public float lookRadius = 20f;
  
    public float attackRadius = 5f;

    public float rangedAttackRadius = 15f;

    [HideInInspector]
    public Transform target;
   
    public bool hasTarget;

    //Scripts And GameObjects
    [SerializeField]
    public GameObject destinationIndicator;

    private EnemyMeleeController meleeController;
    private ProjectileManager rangedController;
    private Vector3 walkpoint;
    private NavMeshAgent agent;
   
    private Vector3 spawnpoint;
   
    //State Machine Variables
    public bool targetReached;

    private void Start()
    {
        if (target == null)
            target = new GameObject().transform;

        agent = GetComponent<NavMeshAgent>();
        
        //initiaslize spawnpoint
        spawnpoint = transform.position;
       
        //initialize the melee Controller 
        meleeController = GetComponent<EnemyMeleeController>();

        //initialize the ranged Controller 
        rangedController = GetComponent<ProjectileManager>();

        HealthEventSystem.current.onDeath += Dead;
        CharacterLoadScript.current.onCharacterSelected += CharacterSelected;
    }

    private void OnDestroy()
    {
        HealthEventSystem.current.onDeath -= Dead;
        CharacterLoadScript.current.onCharacterSelected -= CharacterSelected;
    }

    private void Dead(int id)
    {
        if (gameObject.GetInstanceID() == id)
        {
            agent.enabled = false;
            EngagementScript.current.Disengage(gameObject.name);
        }
    }


    public void Patrol()
    {
        float distance = Vector3.Distance(walkpoint, transform.position);
        if (distance < agent.stoppingDistance)
        {
    
           Stop();
        }
    }


    public void Stop()
    {
        hasTarget = false;
        if (agent.enabled)
            agent.ResetPath();
    }
  
    public void SearchForTarget()
    {
        if (!agent.enabled)
            return;
        
        float randomZ = Random.Range(-lookRadius, lookRadius);
        float randomX = Random.Range(-lookRadius, lookRadius);

        walkpoint = new Vector3(spawnpoint.x + randomX, spawnpoint.y, spawnpoint.z + randomZ);

        findPoint(walkpoint);
    }

    public void SearchForTargetNearPlayer()
    {
        if (!agent.enabled || target == null)
            return;

        //square around player
        float randomZ = Random.Range(-2, 2);
        float randomX = Random.Range(-2, 2);

        //offset so enemy is not too close to player
        if (randomZ > 0) randomZ += lookRadius/2;
        else randomZ -= lookRadius / 2;

        if (randomX > 0) randomX += lookRadius / 2;
        else randomX -= lookRadius / 2;

        walkpoint = new Vector3(target.transform.position.x + randomX, transform.position.y , target.transform.position.z + randomZ);

        findPoint(walkpoint);
    }

    private NavMeshHit findPoint(Vector3 walkpoint)
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(walkpoint, out hit, transform.position.y + 2, NavMesh.AllAreas))
        {
            //Destroy(Instantiate(destinationIndicator, hit.position + new Vector3(0, 0.6f, 0), destinationIndicator.transform.rotation), 2f);
            agent.SetDestination(hit.position);
            hasTarget = true;
        }
        return hit;
    }

    public void Chase()
    {
        if (!agent.enabled || target == null)
            return;

        agent.SetDestination(target.position);
    }

    public void Relocate()
    {
        if (!agent.enabled)
            return;

        if (!hasTarget)
        {
            SearchForTargetNearPlayer();
        }
        else
        {
            float distance = Vector3.Distance(walkpoint, transform.position);

            if (distance < agent.stoppingDistance)
            {
                targetReached = true;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (agent == null)
            return;

        agent = GetComponent<NavMeshAgent>();
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, agent.stoppingDistance);
    }

    public void RotateTowards(Vector3 destination)
    {
        Vector3 lookPos = destination - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.eulerAngles = rotation.eulerAngles;
    }

    public void Attack()
    {
        if (!agent.enabled)
            return;

        meleeController.canAttack = true;
    }

    public void AttackRanged()
    {
        if (!agent.enabled)
            return;

        rangedController.canAttack = true;
    }

    private void CharacterSelected(SelectedCharacterScript.Character character, PlayerMovementScript playerMovementScript)
    {
        if (target != null)
            Destroy(target.gameObject, 0.5f);

        target = playerMovementScript.gameObject.transform;
    }
}