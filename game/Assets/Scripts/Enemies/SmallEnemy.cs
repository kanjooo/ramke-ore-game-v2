using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SmallEnemy : MonoBehaviour
{
    [Header("Small enemy stats")]
    public int Maxhp=10;
    public int Damage=5;
   
    public int currenthp;

    void Start()
    {
        currenthp = Maxhp;
    }

    private void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        currenthp -= damage;

        if (currenthp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
