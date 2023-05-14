using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp=10;
    public int currenthp;
    public int Damage=5;

    void Start()
    {
        currenthp = hp;
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
