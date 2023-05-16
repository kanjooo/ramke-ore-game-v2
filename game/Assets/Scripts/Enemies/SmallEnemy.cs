using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SmallEnemy : MonoBehaviour
{
    [Header("Small enemy stats")]
    public int hp=10;
    public int Damage=5;
   
    private int currenthp;

    void Start()
    {
        currenthp = hp;
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
