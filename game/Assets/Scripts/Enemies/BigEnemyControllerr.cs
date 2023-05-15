using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class BigEnemyControllerr : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    CameraShake shakeCamera;

    public new Animation animation;

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
        player = GameObject.Find("FirstPersonController").transform;
        agent = GetComponent<NavMeshAgent>();
        animation = agent.GetComponent<Animation>();
        shakeCamera = player.GetComponent<CameraShake>();
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
        animation.Play("Walk");
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
        var chaseSound = gameObject.GetComponent<ChaseSound>();
        chaseSound.playSound();
        agent.GetComponent<Animation>().Play("Run");
        agent.speed = 3;
        shakeCamera.ShakeScreen();
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        animation.Play("Attack1");
        shakeCamera.ShakeScreen();
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            Vector3 relativePos = transform.position - player.position;

            // Apply the offset to the relative position
            relativePos += Vector3.up * 2f;

            // Calculate the rotation based on the modified relative position
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            player.rotation = rotation;

            ///Attack code here
            var movement = player.GetComponent<FirstPersonController>();
            movement.enabled = false;
            RestartSceneWithDelay(1.5f);
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
