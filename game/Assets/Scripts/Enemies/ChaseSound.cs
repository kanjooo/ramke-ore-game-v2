using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseSound : MonoBehaviour
{
    public AudioSource chaseSound;
    public BigEnemyControllerr ghoul;
    void Start()
    {
        ghoul = GetComponent<BigEnemyControllerr>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ghoul.playerInSightRange)
        {;
            chaseSound.enabled = true;
            if (chaseSound.isPlaying)
            {
                // let the audio finish playing
            }
        }
        else
        {
            if (!chaseSound.isPlaying)
            {
                chaseSound.enabled = true;
            }
        }
    }
}
