using System;
using System.Collections;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public event Action<Sprite> Died;
    public bool alive = true;
    public float health = 100f;

    public void TakeDamage(float damage, Sprite attackerSprite)
    {
        health -= damage;

        if (health <= 0)
        {
            Die(attackerSprite);
        }
    }

    void Die(Sprite attackerSprite)
    {
        //If we are already dead, we do not need to call this function again
        if (!alive) return;
        alive = false;
        Died?.Invoke(attackerSprite);
    }
}
