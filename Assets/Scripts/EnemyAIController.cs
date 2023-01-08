using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIController : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject player;
    public float sightRange;
    public float attackRange;
    public float walkPointRange;
    public Vector3 walkPoint; //point that an enemy will walk towards
    public LayerMask whatIsPlayer, whatIsGround, whatIsObstacle; //variables that start with whatIs = LayerMask variables (apparently)
    private bool isWalkPointSet = false;
    private bool canAttack = true;
    public float attackDelay = 4;

    void Start()
    {
        player = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        bool isPlayerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        bool isPlayerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        //walk around the map without chasing the player
        if (!isPlayerInSightRange && !isPlayerInAttackRange)
            EnemyPatrolling();
        //when a player gets too close to the enemy, it starts chasing him
        if (isPlayerInSightRange && !isPlayerInAttackRange)
            EnemyChasing();
        //when the enemy reaches the player, it stops moving
        if (isPlayerInAttackRange && isPlayerInSightRange)
            EnemyAttacking();
    }

    private void FindWalkPoint()
    {
        //get a random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        //Is walkPoint outside of the map?
        //arguments - origin of ray, direction of ray, length of ray, what it has to collide with 
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround) && !Physics.Raycast(walkPoint, transform.up, 2f, whatIsObstacle) && !Physics.Raycast(walkPoint, -transform.up, 2f, whatIsObstacle)) {
            isWalkPointSet = true;
        }
    }

    void EnemyPatrolling()
    {
        agent.speed = 4;
        if (isWalkPointSet) {
            agent.SetDestination(walkPoint);
        }
        else {
            FindWalkPoint();
        }
        //When enemy reaches a walkPoint, find a new one
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if (distanceToWalkPoint.magnitude < 5f)
            isWalkPointSet = false;
    }

    void EnemyChasing()
    {
        agent.speed = 10;
        //move towards the player
        agent.SetDestination(player.transform.position);
    }

    void EnemyAttacking()
    {
        //enemy stops moving
        agent.SetDestination(transform.position);

        if(canAttack){
            //Attack the player
            
            Debug.Log("Enemy attacked");
            player.GetComponent<PlayerController>().TakeDamage(2);
            canAttack = false;
            //call this function after attackDelay time
            Invoke(nameof(ResetAttack), attackDelay);
        }
    }

    void ResetAttack(){
        canAttack = true;
    }
}
