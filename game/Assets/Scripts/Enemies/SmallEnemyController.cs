using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SmallEnemyController : MonoBehaviour
{
    //set the values in the inspector
    public Transform target; 
    public float speed;
    public float viewRadius = 15;             
    public float viewAngle = 90;


    private bool playerInSight = false;
    public LayerMask playerMask;                    //  To detect the player with the raycast
    public LayerMask obstacleMask;

    Vector3 startPos;
    public NavMeshAgent agent;
    private void Awake()
    {
        startPos = this.transform.position;
        agent = GetComponent<NavMeshAgent>();
    }


    public void Update()
    {
        Collider[] playerInRange = Physics.OverlapSphere(transform.position, viewRadius, playerMask);
        EnviromentView();
        //check if it is within the range you set
        if (playerInSight)
        {
            agent.SetDestination(target.transform.position);
            print("in sight");
        }
        else
        {
            agent.SetDestination(startPos);
            print("not in sight");
        }
    }

    void EnviromentView()//
    {  
        Collider[] playerInRange = Physics.OverlapSphere(transform.position, viewRadius, playerMask);   //  Make an overlap sphere around the enemy to detect the playermask in the view radius

        for (int i = 0; i < playerInRange.Length; i++)
        {
            Transform player = playerInRange[i].transform;

            Vector3 dirToPlayer = (player.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToPlayer) < viewAngle / 2)
            {
                float dstToPlayer = Vector3.Distance(transform.position, player.position);          //  Distance of the enmy and the player
                if (!Physics.Raycast(transform.position, dirToPlayer, dstToPlayer, obstacleMask))
                {
                    playerInSight = true;             //  The player has been seeing by the enemy and then the nemy starts to chasing the player
                }
                else
                {
                    /*
                     *  If the player is behind a obstacle the player position will not be registered
                     * */
                    playerInSight = false;
                }
            }
            if (Vector3.Distance(transform.position, player.position) > viewRadius)
            {
                /*
                 *  If the player is further than the view radius, then the enemy will no longer keep the player's current position.
                 *  Or the enemy is a safe zone, the enemy will no chase
                 * */
                playerInSight = false;                //  Change the sate of chasing
            }
        }
    }
}
