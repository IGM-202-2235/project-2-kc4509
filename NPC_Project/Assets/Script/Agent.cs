using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class Agent : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    protected PhysicObject physicObject;

    protected Vector3 totalForces = Vector3.zero;
    public float maxForce = 3f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 sterringForce = CaluculateSteeringForces();
        totalForces = CaluculateSteeringForces();
        totalForces = Vector3.ClampMagnitude(totalForces, maxForce);
        physicObject.ApplyForce(totalForces);
        totalForces = Vector3.zero;
    }

    public Vector3 Seek(Vector3 targetPos)
    {
        // Calculate desired velocity
        Vector3 desiredVelocity = targetPos - transform.position;

        // Set desired = max speed
        desiredVelocity = desiredVelocity.normalized * physicObject.maxSpeed;

        // Calculate seek steering force
        Vector3 seekingForce = desiredVelocity - physicObject.Velocity;

        // Return seek steering force
        return seekingForce;
    }
    public Vector3 Flee(Vector3 targetPos)
    {
        // Calculate desired velocity
        Vector3 desiredVelocity = transform.position - targetPos;

        // Set desired = max speed
        desiredVelocity = desiredVelocity.normalized * physicObject.maxSpeed;

        // Calculate seek steering force
        Vector3 fleeForce = desiredVelocity - physicObject.Velocity;

        // Return seek steering force
        return fleeForce;
    }


    public Vector3 Seek(Agent target)
    {
        return Seek(target.transform.position);
    }

    public Vector3 Flee(Agent target)
    {
        return Flee(target.transform.position);
    }

    //Flee and evade & seek and pursue is similar and doesn't' count as two seperate for project 3
    public Vector3 Evade(Agent target)
    {
        return Flee(target.CalculateFuturePosition(5f));
    }

    public Vector3 Wanderer(float time, float radius)
    {
        Vector3 futurePosition = CalculateFuturePosition(time);
        float randAngle = Random.Range(0f, Mathf.PI * 2f);
        Vector3 wanderTarget = futurePosition;
        wanderTarget.x += Mathf.Cos(randAngle) * radius;
        wanderTarget.y += Mathf.Sin(randAngle) * radius;
        return Seek(wanderTarget);
    }

    protected abstract Vector3 CaluculateSteeringForces();

    public Vector3 CalculateFuturePosition(float futureTime)
    {
        return transform.position + (physicObject.Velocity * futureTime);
    }

    //if in bound then apply force steering force to something
    public Vector3 StayInBound()
    {
        Vector3 steeringForce = Vector3.zero;
        //use physicObject to check if out of bound
        //if in bound then run code below
        if (!physicObject.CheckCollision())
        {
            steeringForce += Seek(Vector3.zero);
        }
        return steeringForce;
    }

    public Vector3 Seperate(List<Agent> targets){
        Vector3 sum = Vector3.zero;
        Vector3 steeringForce = Vector3.zero;
        float desiredSeparation = 2;
        int count = 0;
        for(int i = 0; i < targets.Count; i++){
            float distance = Vector3.Distance(transform.position,targets[i].transform.position);
            if(this != targets[i] && distance < desiredSeparation){
                Vector3 diff = transform.position - targets[i].transform.position;
                diff.Normalize();
                sum += diff;
                count++;
            }
        }
        if(count > 0){
            sum = sum/count;
            sum.Normalize();
            sum = sum * maxForce;
            steeringForce = sum - physicObject.Velocity;
        }
        
        return steeringForce;
    }

    public Vector3 Align(List<Agent> targets){
        Vector3 alignForce = Vector3.zero;
        Vector3 steeringForce = Vector3.zero;
        for(int i = 0; i < targets.Count; i++){
            alignForce += targets[i].physicObject.Velocity;
        }
        alignForce = alignForce/targets.Count;
        alignForce.Normalize();
        alignForce = alignForce * maxForce;
        steeringForce = alignForce - physicObject.Velocity;
        return steeringForce;
    }

    public Vector3 Cohesion(List<Agent> targets){
        Vector3 cohesionForce = Vector3.zero;
        int neighborDistance = 2;
        int count = 0;
        for(int i = 0; i < targets.Count; i++){
            float distance = Vector3.Distance(transform.position, targets[i].transform.position);
            if((this != targets[i]) && (distance < neighborDistance)){
                cohesionForce += targets[i].transform.position;
                count++;
            }
        }
        if(count > 0){
            cohesionForce = cohesionForce/count;
            return Seek(cohesionForce);
        }
        else{
            return Vector3.zero;
        }

    }

}
