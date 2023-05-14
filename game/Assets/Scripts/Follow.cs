using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.SceneManagement;

public class Follow : MonoBehaviour
{
    [Header("Player and Agent")]
    public NavMeshAgent agent;
    public Transform player;
    public PlayerManager playerManager;

    [Header("Misc")]
    public LayerMask whatIsGround, whatIsPlayer;

    [Header("Enemy")]
    public float timeBetweenAttacks;
    public float health;
    bool alreadyAttacked;
    public Animation animation;
    private Transform enemyTransform;
    public float escapeTotaltime = 0.5f;
    private float escapeTimeRemaining;
    bool isAttacking;
    //Patroling
    private Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        playerManager = player.GetComponent<PlayerManager>();
        animation = GetComponent<Animation>();
        animation.Play("Idle");
        enemyTransform = transform;
        escapeTimeRemaining = escapeTotaltime;
    }
    void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange)
        {
            AttackPlayer();
        }       
    }
    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);


        transform.LookAt(player);
        Vector3 currentRotation = enemyTransform.localEulerAngles;
        currentRotation.x = 0f;
        currentRotation.z = 0f;
        enemyTransform.localEulerAngles = currentRotation;

        if (!alreadyAttacked)
        {
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
            animation.Play("Attack1");
            playerManager.TakeDamage(10);
        }
    }
    private void Patroling()
    {
        agent.speed = 2f;
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
        animation.Play("Walk");
    }
    private void ChasePlayer()
    {
        agent.speed = 5f;
        agent.SetDestination(player.position);
        animation.Play("Run");
    }

    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
        isAttacking = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

}
//