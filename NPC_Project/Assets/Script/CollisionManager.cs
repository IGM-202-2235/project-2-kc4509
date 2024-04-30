using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    public SpriteInfo blueShip;
    public SpriteInfo redShip;
    public SpawnManager spawnManager;
    public List<Agent> fishesHit;
    public bool isHit;
    // Start is called before the first frame update
    void Start()
    {
        isHit = false;
    }

    // Update is called once per frame
    void Update()
    {
        blueShipCollision();
        redShipCollision();
    }

    bool AABBCollsionCheck(SpriteInfo spriteA, Agent spriteB)
    {
        if (spriteB.min.x < spriteA.max.x &&
        spriteB.max.x > spriteA.min.x &&
        spriteB.max.y > spriteA.min.y &&
        spriteB.min.y < spriteA.max.y)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void blueShipCollision(){
        for (int i = 0; i < spawnManager.spawnedFishes.Count; i++){
            if (AABBCollsionCheck(blueShip, spawnManager.spawnedFishes[i])){
                print("is hit by blue!!");
                spawnManager.spawnedFishes[i].spriteRenderer.color = Color.blue;
            }
        }
    }

    void redShipCollision(){
        for (int i = 0; i < spawnManager.spawnedFishes.Count; i++){
            if (AABBCollsionCheck(redShip, spawnManager.spawnedFishes[i])){
                print("is hit by red!!");
                spawnManager.spawnedFishes[i].spriteRenderer.color = Color.red;
            }
        }
    }
}
