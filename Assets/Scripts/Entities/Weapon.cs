using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    Entity source;
    Entity target;
    public float damage;
    public void Init(Entity source_init, Entity target_init)
    {
        source = source_init;
        target = target_init;
        transform.position = source_init.gameObject.transform.position;
        transform.LookAt(target.gameObject.transform.position,
                         Vector3.back);

    }

    private void Update()
    {
        transform.LookAt(target.gameObject.transform.position,
                         Vector3.back);

        transform.Translate(Vector3.forward * Time.deltaTime * 30.0f);

        if (Vector3.Distance(transform.position,
                             target.gameObject.transform.position)
            < 0.1f)
        {
            target.Damage(damage, source, this);
            Destroy(this.gameObject);
        }
    }
}
