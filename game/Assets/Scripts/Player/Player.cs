using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Player stats")]
    public int Maxhp = 20;
    public int currenthp;
    public Hp_BarScript healthbar;


    private void Awake()
    {

    }
    void Start()
    {
        currenthp = Maxhp;
        healthbar.SetMaxHp(Maxhp);
    }
    public void TakeDamage(int damage)
    {
        currenthp -= damage;
        healthbar.SetHealth(currenthp);
        if (currenthp <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void IncreaseHealth(int value)
    {
        currenthp += value;
        currenthp = Mathf.Min(currenthp, Maxhp);
        healthbar.SetHealth(currenthp);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            TakeDamage(2);
        }
        print(currenthp);
    }
}
