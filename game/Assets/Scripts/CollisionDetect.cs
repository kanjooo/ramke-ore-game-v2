using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetect : MonoBehaviour
{

    public KatanaController weapon;
    bool isColiding = false;

    public void OnTriggerEnter(Collider other)
    {
        
        Enemy en = other.GetComponent<Enemy>();
        if (isColiding) return;
        if (other.tag == "SmallEnemy" && weapon.IsAttacking)
        {
         en.TakeDamage(weapon.Damage);
            isColiding = true;
            StartCoroutine(Reset());
        }
    }
    IEnumerator Reset()
    {
        yield return new WaitForSeconds(weapon.AttackCooldown);
        isColiding = false;
    }

}
