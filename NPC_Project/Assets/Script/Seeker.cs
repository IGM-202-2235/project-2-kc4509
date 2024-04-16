using System.Collections.Generic;
using UnityEngine;

public class Seeker : Agent
{
    [SerializeField]
    Agent target;

    public float seekWeight = 1f, boundWeight = 1f;

    protected override Vector3 CaluculateSteeringForces()
    {
        Vector3 seekForce = Seek(target) * seekWeight;
        Vector3 boundForce = StayInBound() * boundWeight;
        return seekForce + boundForce;
    }
}
