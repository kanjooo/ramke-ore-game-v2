using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Player stats")]
    
    public int damage;
    private int hp;

    public int health
    {
        get { return hp; }
        set { hp = value; }
    }
}
