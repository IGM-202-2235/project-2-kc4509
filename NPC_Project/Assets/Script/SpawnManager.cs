using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    // (Optional) Prevent non-singleton constructor use.
    protected SpawnManager() { }

    public Agent starAgent;
    public Agent fishAgent;
    public List<SpriteInfo> fishes;

    public enum EnemyTypes
    {
        Car,
        Robot
    }
    public List<Obstacle> obstacles = new List<Obstacle>();
    public int fishCount = 10;
    public int starCount = 8;
    public List<Agent> spawnedFishes;
    public List<Agent> stars;
    public List<SpriteRenderer> fishesRender;

    // Start is called before the first frame update
    void Start()
    {
        spawn();
    }

    // Update is called once per frame
    void Update()
    {

    }
    float Gaussian(float mean, float stdDev)
    {
        float val1 = Random.Range(0f, 1f);
        float val2 = Random.Range(0f, 1f);

        float gaussValue =
                 Mathf.Sqrt(-2.0f * Mathf.Log(val1)) *
                 Mathf.Sin(2.0f * Mathf.PI * val2);

        return mean + stdDev * gaussValue;
    }

    public void spawn()
    {
        cleanup();

        for (int i = 0; i < fishCount; i++)
        {
            spawnFish();
        };
        for (int i = 0; i < starCount; i++)
        {
            spawnStars();
        };

    }

    public void spawnFish()
    {
        Agent newFish = Instantiate(fishAgent);
        Camera cam = Camera.main;
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;
        //width & height divided by 8 for standard deviations
        float x = Gaussian(0, width / 8);
        float y = Gaussian(0, height / 8);
        newFish.transform.position = new Vector3(-x, y, 0);
        newFish.transform.rotation = Quaternion.identity;


        spawnedFishes.Add(newFish);
    }

    public void spawnStars()
    {
        Agent newStar = Instantiate(starAgent);
        Camera cam = Camera.main;
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;
        //width & height divided by 8 for standard deviations
        float x = Gaussian(0, width / 8);
        float y = Gaussian(0, height / 8);
        newStar.transform.position = new Vector3(-x, y, 0);
        newStar.transform.rotation = Quaternion.identity;
        stars.Add(newStar);
    }

    void cleanup()
    {
        foreach (Agent newFish in spawnedFishes)
        {
            //Only destroys the spriteRender and not the gameObject
            //Will not clean up reference when calling destroyed
            //Need to include .gameObject
            Destroy(newFish.gameObject);
        }

        spawnedFishes.Clear();

        foreach (Agent newstar in stars)
        {
            //Only destroys the spriteRender and not the gameObject
            //Will not clean up reference when calling destroyed
            //Need to include .gameObject
            Destroy(newstar.gameObject);
        }

        stars.Clear();
    }
}
