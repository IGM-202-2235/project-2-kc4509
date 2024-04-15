using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flocking :  Agent
{
    [SerializeField]
    SpawnManager spawnManager;
    public float wanderTime, wanderRadius;
    public float wanderWeight = 1f, boundWeight = 1f, seperateWeight = 1f, alignWeight = 1f, cohesionWeight =1f;

    protected override Vector3 CaluculateSteeringForces()
    {
        Vector3 seperateForce = Seperate(spawnManager.spawnedEnemies) * seperateWeight;
        Vector3 cohesionForce = Cohesion(spawnManager.spawnedEnemies) * cohesionWeight;
        Vector3 wanderForce = Wanderer(wanderTime, wanderRadius) * wanderWeight;
        Vector3 alignForce = Align(spawnManager.spawnedEnemies) * alignWeight;
        Vector3 boundsForce = StayInBound() * boundWeight;
        

        return seperateForce + wanderForce + boundsForce + alignForce + cohesionForce;
    }
}