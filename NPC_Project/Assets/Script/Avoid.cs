using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avoid : Agent
{
    [SerializeField]
    float wanderTime, wanderRadius, avoidRadius;
    public float wanderWeight = 1f, boundWeight = 1f, obstacleWeight =1f;

    protected override Vector3 CaluculateSteeringForces()
    {
        Vector3 wanderForce = Wanderer(wanderTime, wanderRadius) * wanderWeight;
        Vector3 boundsForce = StayInBound() * boundWeight;
        Vector3 avoidForce = AvoidObstacles() * obstacleWeight;
        
        return wanderForce + boundsForce + avoidForce;
    }

    private void OnDrawGizmosSelected()
    {
        #region Wander
        Gizmos.color = Color.magenta;

        Vector3 futurePosition = CalculateFuturePosition(wanderTime);
        Gizmos.DrawWireSphere(futurePosition, wanderRadius);

        Gizmos.color = Color.cyan;
        float randAngle = Random.Range(0f, Mathf.PI * 2f);

        Vector3 wanderTarget = futurePosition;

        wanderTarget.x += Mathf.Cos(randAngle) * wanderRadius;
        wanderTarget.y += Mathf.Sin(randAngle) * wanderRadius;

        Gizmos.DrawLine(transform.position, wanderTarget);
        #endregion

        //  Found Obstacle Lines
        Gizmos.color = Color.yellow;

        foreach(Vector3 obPos in foundObstaclePositions)
        {
            Gizmos.DrawLine(transform.position, obPos);
        }

        //
        //  Draw safe space box
        //
        Gizmos.color = Color.green;
        Vector3 futurePos = CalculateFuturePosition(wanderTime);

        float length = Vector3.Distance(transform.position, futurePos) + physicObject.radius;


        Vector3 boxSize = new Vector3(physicObject.radius * 2f, length, 1f);

        Vector3 boxCenter = Vector3.zero;
        boxCenter.y += length / 2f;
        //transform.rotation = Quaternion.LookRotation(Vector3.back, direction);

        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(boxCenter, boxSize);
        Gizmos.matrix = Matrix4x4.identity;
    }
}
