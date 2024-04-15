using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    // (Optional) Prevent non-singleton constructor use.
    protected SpawnManager() { }

    public Agent agent;

    public enum EnemyTypes
    {
        Car,
        Robot
    }

    public List<Agent> enemies;

    public int enemyCount = 5;
    public List<Agent> spawnedEnemies;

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

        for (int i = 0; i < enemyCount; i++)
        {
            spawnEnemy();
        };
    }

    public void spawnEnemy()
    {
        Agent newEnemy = Instantiate(agent);
        //GameObject newEnemy;
        //newEnemy.color = Random.ColorHSV(0, 1, 1, 1, 1, 1);
        Camera cam = Camera.main;
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;
        //width & height divided by 8 for standard deviations
        float x = Gaussian(0, width / 8);
        float y = Gaussian(0, height / 8);
        newEnemy.transform.position = new Vector3(-x, y, 0);

        spawnedEnemies.Add(newEnemy);
    }

    void cleanup()
    {
        foreach (Agent newEnemy in spawnedEnemies)
        {
            //Only destroys the spriteRender and not the gameObject
            //Will not clean up reference when calling destroyed
            //Need to include .gameObject
            Destroy(newEnemy.gameObject);
        }

        spawnedEnemies.Clear();
    }
}
