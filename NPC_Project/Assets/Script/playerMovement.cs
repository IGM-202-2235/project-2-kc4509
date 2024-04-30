using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{

    public Vector3 objectPosition = new Vector3(0, 0, 0);

    public float speed = 4f;

    Vector3 velocity = Vector3.zero;

    public Vector3 direction = new Vector3(0, 0, 0);
    public Camera cam;

    public Vector3 Direction
    {
        get { return direction; }
        set { direction = value.normalized; }
    }
    public void SetDirection(Vector3 inputDirection)
    {
        direction = inputDirection;
    }
    // Start is called before the first frame update
    void Start()
    {
        objectPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        velocity = direction * speed * Time.deltaTime;
        objectPosition += velocity;

        transform.position = objectPosition;

        //Rotation
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        }


        //Camera size

        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;

        if (objectPosition.x > width / 2)
        {
            objectPosition.x = width / 2;
        }
        if (objectPosition.x < -width / 2)
        {
            objectPosition.x = -width / 2;
        }
        if (objectPosition.y > height / 2)
        {
            objectPosition.y = height / 2;
        }
        if (objectPosition.y < -height / 2)
        {
            objectPosition.y = -height / 2;
        }
    }
}
