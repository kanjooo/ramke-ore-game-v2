using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class SmallEnemyController : MonoBehaviour
{

    public NavMeshAgent agent;

    public Transform playerTransform;

    public Player player;

    Animator animator;

    CameraShake shakeCamera;

    public float zOffset = 0f;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health;
    public bool isChasing = false;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        playerTransform = GameObject.Find("FirstPersonController").transform;
        player = GetComponent<Player>();
        agent = GetComponent<NavMeshAgent>();
        shakeCamera = playerTransform.GetComponent<CameraShake>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }

    private void Patroling()
    {
        animator.Play("Z_Walk");
        agent.speed = 2;
        shakeCamera.StopShakeScreen();
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
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

    private void ChasePlayer()
    {
        animator.Play("Z_Run");
        agent.GetComponent<Animation>().Play("Run");
        agent.speed = 3;
        shakeCamera.ShakeScreen();
        agent.SetDestination(playerTransform.position);
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(playerTransform);

        if (!alreadyAttacked)
        {
            ///Attack code here
            animator.Play("Z_Attack");
            player.TakeDamage(2);
            ///End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }


    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    public void RestartSceneWithDelay(float delay)
    {
        StartCoroutine(RestartSceneCoroutine(delay));
    }

    private IEnumerator RestartSceneCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
