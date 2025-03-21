using UnityEngine;
using UnityEngine.UIElements;

public class SpawnManager : MonoBehaviour
{
    public Transform[] spawnPoint;  //스폰 지점
    public SpawnData[] spawnData;   //스폰 몬스터 데이터

    int level;      //몬스터 레벨
    float timer;

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        //타이머를 통해 일정 시간이 지난 후 몬스터 레벨 상승(현재 120초)
        timer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 10f), spawnData.Length -1) ;

        if (timer > spawnData[level].spawnTime)
        {
            timer = 0;
            Spawn();
        }
        
    }
    public void Spawn()
    {
        //GameObject enemy = GameManager.instance.pool.Get(Random.Range(0,2));
        GameObject enemy = GameManager.instance.pool.Get(0);
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        enemy.GetComponent<Enemy>().Init(spawnData[level]);
    }
}

[System.Serializable]
public class SpawnData
{
    public float spawnTime;
    public int spriteType;
    public int monsterHp;
    public float speed;
}