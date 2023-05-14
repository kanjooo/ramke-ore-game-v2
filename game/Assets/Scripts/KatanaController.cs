using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatanaController : MonoBehaviour
{

    public GameObject Katana;
    public bool CanAttack = true;
    public float AttackCooldown = 1.0f;
    public int Damage = 5;
    public bool IsAttacking;
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(CanAttack)
            {
                KatanaAttack();
            }
        }
    }

    public void KatanaAttack()
    {
        IsAttacking = true;
        CanAttack = false;
        Animator anim = Katana.GetComponent<Animator>();
        anim.SetTrigger("Attack");
        StartCoroutine(ResetAttackCooldown());
    }

    IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(AttackCooldown);
        CanAttack = true;
    }

}
