using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class SmallEnemyController : MonoBehaviour
{
    [Header("Enemy")]
    public NavMeshAgent agent;
    public float speed;
    public float damage;
    public float knockbackPower = 5f;
    [Header("Player")]
    public GameObject player;
    public LayerMask playerLayer;

    [Header("Enemy stats")]
    public float sightRange; 
    public float attackRange;
    public float timeBetweenAttacks;

    [Header("Misc stuff")]
    
    private bool playerInSightRange, playerInAttackRange;
    private bool alreadyAttacked = false;
    Animator animator;
    private Vector3 startPoint;
    private bool isKnockbackActive = false;
    private Vector3 knockbackDirection;
    Player playerController;


    private void Start()
    {
        //Getting components
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        playerController = player.GetComponent<Player>();
        //
        startPoint = agent.transform.position;

    }
    private void Update()
    {
        LookForPlayer();
        if (playerInSightRange && !playerInAttackRange)
        {
            Chase();
        }
        if(playerInSightRange && playerInAttackRange)
        {
            Attack();
        }
        if(!playerInSightRange && !playerInAttackRange)
        {
            ReturnHome();
        }
        
    }
    private void ReturnHome()//
    {
        animator.SetBool("isChasing", false);
        print("Returning home!");
        agent.SetDestination(startPoint);
    }

    private void LookForPlayer()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);
    }

    private void Chase()
    {
        animator.SetBool("isChasing", true);
        print("Chasing!");
        print(player.transform.position);
        agent.SetDestination(player.transform.position);
    }
    private void Attack()
    {
        if (!alreadyAttacked)
        {
            print("Attacking!");

            animator.SetBool("isAttacking", true);
            // Stopping agent
            agent.isStopped = true;

            alreadyAttacked = true;
            StartCoroutine(DodgeTime());
        }
    }

    private IEnumerator DodgeTime()
    {
        yield return new WaitForSeconds(0.5f); // Wait for 1 second

        // Knockback
        if (playerInAttackRange)
        {
            Rigidbody playerRigidbody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
            Vector3 knockbackDirection = playerRigidbody.transform.position - transform.position;
            knockbackDirection.y = 0f; // Optional: Set the y-component to zero to prevent vertical knockback
            playerRigidbody.AddForce(knockbackDirection.normalized * knockbackPower, ForceMode.Impulse);
            playerController.TakeDamage(5);

        }
        Invoke(nameof(ResetAttack), timeBetweenAttacks);
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
        agent.isStopped = false;
        animator.SetBool("isAttacking", false);
    }
}
