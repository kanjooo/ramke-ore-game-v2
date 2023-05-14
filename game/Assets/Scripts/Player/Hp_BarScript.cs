using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Hp_BarScript : MonoBehaviour
{
    public Slider slider;
            
    public void SetHealth(int health)
    {
        slider.value = health;
    }

    public void SetMaxHp(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
}
