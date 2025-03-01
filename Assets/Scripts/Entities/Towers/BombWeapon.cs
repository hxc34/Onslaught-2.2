using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombWeapon : Weapon
{
    Vector3 projectileTarget;

    protected override void Update()
    {
        transform.LookAt(projectileTarget,
         Vector3.back);
        
        transform.Translate(Vector3.forward * Time.deltaTime * 30.0f);
        
        if (Vector3.Distance(transform.position, projectileTarget) < 0.1f)
        {
            Destroy(this.gameObject);
        }
    }
}
