using UnityEngine;
using System.Linq;

public class SpawnManager : MonoBehaviour
{
    public Transform[] spawnPoint;  //스폰 지점
    public SpawnData[] spawnData;   //스폰 몬스터 데이터

    int level;      //몬스터 레벨
    float timer;

    bool bossChk = false;   //보스 체크

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        //타이머를 통해 일정 시간이 지난 후 몬스터 레벨 상승(현재 120초)
        //10초마다 스폰 몬스터 레벨 상승(spawnData Array 내부에서 다음 내용으로)
        timer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt( (GameManager.instance.gameTime) / 10f), spawnData.Length - 1);

        //최종 레벨에 도달하는 경우
        if (level >= spawnData.Length - 1)
        {
            switch (bossChk)
            {
                case false : //보스레벨 도달 시 1회 스폰 후 bossChk : true
                    Debug.Log("보스 등장 시기");
                    GameObject.FindGameObjectsWithTag("Enemy").ToList().ForEach(x => x.SetActive(false));
                    Debug.Log("전 몬스터 제거");
                    Debug.Log($"보스 스폰 {level}");
                    bossChk = true;
                    timer = 0;
                    Spawn();
                    break;
                case true : //보스 스폰 이후(bossChk : true) 레벨 보정
                    level = 2;
                    break;
            }
        }

        //레벨을 기준으로 스폰 몬스터 수준 결정
        //몬스터 스폰 최소시간보다 타이머가 더 크면 스폰, 타이머 리셋
        if (timer > spawnData[level].spawnTime && bossChk == false)
        {
            Debug.Log($"현재 난이도 {level}");
            timer = 0;
            Spawn();
        }
    }
    public void Spawn()
    {
        if (bossChk) spawnData[level].boss = true;
        //GameManager에 존재하는 PoolManager의 0번 풀에서 비활성화된 오브젝트를 가져옴.
        //아마 0번 풀이 몬스터 목록으로 인스펙터에서 정의될듯.
        GameObject enemy = GameManager.instance.pool.Get(0);
        //미리 지정된 스폰 포인트 목록 중 한 곳을 지정
        enemy.transform.position = spawnPoint[Random.Range(0, spawnPoint.Length)].position;
        //몬스터 레벨에 따라 초기화된 몬스터 생성
        enemy.GetComponent<Enemy>().Init(spawnData[level]);
    }
}

[System.Serializable]
public class SpawnData
{
    public float spawnTime;
    public int spriteIndex;
    public int monsterHp;
    public float speed;
    public bool boss;
}