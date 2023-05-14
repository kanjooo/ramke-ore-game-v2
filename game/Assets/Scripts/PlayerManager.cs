using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    [Header("Player stats")]
    
    public int hp = 20;
    public int currenthp;
    public Hp_BarScript healthbar;
    void Start()
    {
        currenthp = hp;
        healthbar.SetMaxHp(hp);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "SmallEnemy")
        {
            Enemy en = other.GetComponent<Enemy>();
            TakeDamage(en.Damage);
            
        }
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

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            TakeDamage(2);
        }
    }
}
