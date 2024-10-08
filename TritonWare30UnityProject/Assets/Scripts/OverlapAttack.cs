using System.Collections.Generic;
using UnityEngine;

public class OverlapAttack
{
    public GameObject attacker;
    public float damage;
    public HitboxGroup hitboxGroup;

    private HashSet<HurtBox> hitHurtboxes;
    //finds all hurtboxes within the given hitbox. Damages only unique hurtboxes.
    //should be constantly updated frame by frame.
    public void Fire()
    {
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
                        hitHurtboxes.Add(hurtbox);
                        hurtbox.healthComponent.TakeDamage(damage);
                    }
                }
            }
        }
    }
}