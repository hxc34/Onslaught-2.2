using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public Weapon weapon;
    public CapsuleCollider range_collider;
    public float attack_cooldown;
    public float health;

    public void Damage(float damage, Entity attacker, Weapon damager)
    {
        //Debug.LogFormat(this + " hit by " + attacker);
        health -= damage;
        // take damage, provided context for attacker
        if (0 >= health) {
            Die();
        }
    }

    private void Die()
    {
        // die.
        //Debug.LogFormat(this + " died");
        Destroy(this.gameObject);
    }

    protected virtual void AttackAnimation()
    {
        // do nothing for base Entity class
        // override for subclasses with animations
    }

    protected void Attack(Entity target)
    {
        Weapon weapon_instance;

        AttackAnimation();

        weapon_instance = Instantiate(weapon);
        weapon_instance.Init(this, target);
    }
}
