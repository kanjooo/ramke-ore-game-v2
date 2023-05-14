using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    [Header("Player stats")]
    public int hp = 20;
    private int currenthp;
    public Hp_BarScript healthbar;
    void Start()
    {
        currenthp = hp;
        healthbar.SetMaxHp(hp);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "SmallEnemy")
        {
            SmallEnemy en = other.GetComponent<SmallEnemy>();
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
        if (Input.GetKeyDown(KeyCode.P))
        {
            TakeDamage(2);
        }
    }
}
