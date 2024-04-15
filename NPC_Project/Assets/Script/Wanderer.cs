using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wanderer :  Agent
{
    [SerializeField]
    float wanderTime, wanderRadius;
    public float wanderWeight = 1f, boundWeight = 1f;

    protected override Vector3 CaluculateSteeringForces()
    {
        Vector3 wanderForce = Wanderer(wanderTime, wanderRadius) * wanderWeight;
        Vector3 boundsForce = StayInBound() * boundWeight;
        
        return wanderForce + boundsForce;
    }
}

