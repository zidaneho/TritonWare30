using System;
using System.Collections;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public bool alive = true;
    public float health = 100f;

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        alive = false;
    }
}
