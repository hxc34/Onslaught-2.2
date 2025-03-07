using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleTower : Entity
{

    public float tower_range;
    public float slow_rate = 0.5f;
    private List<Enemy> targets;

    void Start()
    {
        targets = new List<Enemy>();

        if (null != range_collider)
        {
            range_collider.radius = tower_range;
        }

       

    }

    void Update()
    {
        // debug show tower range
        for (float i = 0; 2 * Mathf.PI > i; i += 2 * Mathf.PI / 16)
        {
            // line between point, next point, red, and last for a frame
            Debug.DrawLine(transform.position + new Vector3(Mathf.Sin(i) * tower_range, 0.0f, Mathf.Cos(i) * tower_range),
                            transform.position + new Vector3(Mathf.Sin(i + 2 * Mathf.PI / 16) * tower_range, 0.0f, Mathf.Cos(i + 2 * Mathf.PI / 16) * tower_range),
                            Color.red,
                            Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Enemy attacker;
        if (null != (attacker = other.GetComponent<Enemy>()))
        {
            targets.Add(attacker);
            attacker.speed = attacker.speed * slow_rate;
        }
        return;
    }

    void OnTriggerExit(Collider other)
    {
        Enemy attacker;
        if (null != (attacker = other.GetComponent<Enemy>()))
        {
            // byebye, come again!
            targets.Remove(attacker);
            attacker.speed = attacker.speed / slow_rate;
        }
    }
}
