using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class Flee : Agent
{
    [SerializeField]
    Agent target;
    public float fleeWeight = 1f, boundWeight = 1f;

    protected override Vector3 CaluculateSteeringForces()
    {
        Vector3 fleeForce = Flee(target) * fleeWeight;
        Vector3 boundForce = StayInBound() * boundWeight;
        return fleeForce + boundForce;
    }
}