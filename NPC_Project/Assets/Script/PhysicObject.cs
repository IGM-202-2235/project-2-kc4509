using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

public class PhysicObject : MonoBehaviour
{
    Vector3 direction, velocity, acceleration;
    public Vector3 position;
   public Vector3 Direction{get{return direction;}}
    public Vector3 Velocity{get{return velocity;}}

    public float mass = 1f, maxSpeed;

    public float MaxSpeed{get{return maxSpeed;}}

    //Things that might go away
    public bool useFriction, useGravity;
    public float coeff, gravityScalar;

    // Start is called before the first frame update
    void Start()
    {
        gravityScalar = 9.8f;
        //Doesn't start object in middle of screen, starts off where the user places the object
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Use functions before
        if (useGravity)
        {
            ApplyGravity(Vector3.down * gravityScalar);
        }
        if (useFriction)
        {
            ApplyFriction();
        }

        // Calculate the velocity for this frame - New
        velocity += acceleration * Time.deltaTime;

        //used to limit how fast a thing can go/ how fast an agent can run
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

        position += velocity * Time.deltaTime;

        // Grab current direction from velocity  - New
        direction = velocity.normalized;
        // print(direction);
        //transform.rotation = Quaternion.LookRotation(direction);

        transform.position = position;

        // Zero out acceleration - New
        acceleration = Vector3.zero;

        Camera cam = Camera.main;
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;

        // if (position.x > width/2){
        //     velocity.x *= -1;
        //     position.x = width/2;
        // }
        // if(position.x < -width/2){
        //     velocity.x *= -1;
        //     position.x = -width/2;
        // }

        // if (position.y > height/2){
        //     velocity.y *= -1;
        //     position.y = height/2;
        // }
        // if(position.y < -height/2){
        //     velocity.y *= -1;
        //     position.y = -height/2;
        // }
        acceleration = Vector3.zero;
    }

    //Force Method
    public void ApplyForce(Vector3 force)
    {
        acceleration += force / mass;
    }

    public void ApplyGravity(Vector3 gravity)
    {
        acceleration += gravity;
    }

    public void ApplyFriction()
    {
        Vector3 friction = velocity * -1;
        friction.Normalize();
        friction = friction * coeff;

        ApplyForce(friction);
    }

    public bool CheckCollision(){
        Camera cam = Camera.main;
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;
        if(position.x < width/2 && position.x > -width/2 && position.y > -height/2 && position.y < height/2){
            return true;
        }
        return false;
    }

}
