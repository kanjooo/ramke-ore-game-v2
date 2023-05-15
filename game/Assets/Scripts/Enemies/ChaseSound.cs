using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseSound : MonoBehaviour
{
    public AudioSource chaseSound;
    public TestBigEnemy ghoul;
    void Start()
    {
        ghoul = GetComponent<TestBigEnemy>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void playSound()
    {
        if (!chaseSound.isPlaying)
        {
            chaseSound.Play();
        }
    }
}
