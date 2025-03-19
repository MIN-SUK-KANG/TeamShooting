using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform[] spawnPoint;
    float timer;

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if(timer > 0.3f)
        {
            timer = 0;
            Spawn();


        }
        
    }
    public void Spawn()
    {
        GameObject enemy = GameManager.instance.pool.Get(Random.Range(0,2));
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
    }
}
