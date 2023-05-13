using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Player stats")]
    
    public int damage = 5;
    public int hp = 20;
    public int currenthp;
    public Hp_BarScript healthbar;
    void Start()
    {
        currenthp = hp;
    }

    void TakeDamage(int damage)
    {
        currenthp -= damage;
        healthbar.SetHealth(currenthp);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            TakeDamage(2);
        }
    }
}
