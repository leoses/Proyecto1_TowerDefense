//España

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Scrollbar Healthbar;
    public float maxHealth;

    public void Damage(float damage)
    {
        maxHealth -= damage;
        Healthbar.size = maxHealth / 100f;
    }
}
