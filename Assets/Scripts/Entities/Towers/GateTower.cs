using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateTower : Entity
{

    public GameObject spawner;

    void Start()
    {

      
        spawner = GameObject.FindWithTag("Enemy Spawner");
        spawner.GetComponent<EnemySpawner>().FunnelEffect(1, 1.25f);
    }
}
