using System.Collections.Generic;
using UnityEngine;

public class OverlapAttack
{
    public GameObject attacker;
    public float damage;
    public HitboxGroup hitboxGroup;

    private HashSet<HurtBox> hitHurtboxes = new HashSet<HurtBox>();
    //finds all hurtboxes within the given hitbox. Damages only unique hurtboxes.
    //should be constantly updated frame by frame.
    //returns true if an enemy is hit
    public bool Fire()
    {
        bool hitEnemy = false;
        if (hitboxGroup == null)
        {
            Debug.LogError("Remember to reference a hitbox group to the attack!");
            return false;
        }
        foreach (var hitbox in hitboxGroup.hitboxes)
        {
            Collider2D[] colliders = Physics2D.OverlapBoxAll(
                attacker.transform.TransformPoint(hitbox.transform.localPosition),
                hitbox.transform.localScale, hitbox.transform.localEulerAngles.z);
            
            foreach (var collider in colliders)
            {
                var hurtbox = collider.GetComponent<HurtBox>();
                if (hurtbox != null && !hitHurtboxes.Contains(hurtbox))
                {
                    var attackerTeam = attacker.GetComponent<TeamComponent>();
                    if (attackerTeam != null && hurtbox.teamComponent.teamIndex != attackerTeam.teamIndex)
                    {
                        hitEnemy = true;
                        hitHurtboxes.Add(hurtbox);
                        hurtbox.healthComponent.TakeDamage(damage);
                    }
                }
            }
        }

        return hitEnemy;
    }
}