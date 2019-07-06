using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Slider healthBar;

    // Update is called once per frame
    void Update()
    {
        healthBar.value = (PlayerStats.Lives / 20f);
    }
}
