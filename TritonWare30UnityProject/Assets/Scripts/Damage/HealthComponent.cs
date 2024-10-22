using System;
using System.Collections;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public event Action Died;
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
        //If we are already dead, we do not need to call this function again
        if (!alive) return;
        alive = false;
        Died?.Invoke();
    }
}
