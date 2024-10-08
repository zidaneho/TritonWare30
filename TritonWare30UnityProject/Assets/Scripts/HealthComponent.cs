using System;
using System.Collections;
using UnityEngine;

public class HurtBox : MonoBehaviour
{
    public HealthComponent healthComponent { get; private set; }
    public TeamComponent teamComponent { get; private set; }

    private void Awake()
    {
        healthComponent = GetComponentInParent<HealthComponent>();
        teamComponent = GetComponentInParent<TeamComponent>();
    }
}

//Identifier class for HitboxGroup. Move, Rotate, and Scale the transform to adjust the hitbox

//Class should be initialized on the start of a attack.

public class HealthComponent : MonoBehaviour
{
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
        
    }
}
