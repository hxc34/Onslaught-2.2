using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 10f; // Placeholder
    public float speed = 2f;   // Movement speed

    // For taking damage later
    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        // Destroy or run an animation
        Destroy(gameObject);
    }
}
