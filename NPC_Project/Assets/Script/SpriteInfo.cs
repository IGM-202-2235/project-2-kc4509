using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteInfo : Singleton<SpriteInfo>
{
    public SpriteRenderer spriteRenderer;

    public Vector3 min, max;



    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = transform.GetComponent<SpriteRenderer>();

        min = spriteRenderer.bounds.min;
        max = spriteRenderer.bounds.max;
    }

    // Update is called once per frame
    void Update()
    {
        //otherwise the box around the object won't move with the object
        min = spriteRenderer.bounds.min;
        max = spriteRenderer.bounds.max;
    }
    private void OnDrawGizmos(){
        //called before playmode
        Gizmos.color = Color.yellow;
        if(spriteRenderer != null)
        {Gizmos.DrawWireCube(transform.position, spriteRenderer.bounds.size);}
    }
}
