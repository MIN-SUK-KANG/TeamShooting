using UnityEngine;
using System.Collections;


public class Spawn_V : MonoBehaviour
{
    public float ss = -11;
    public float es = 11;
    public float StartTime = 3;
    public float SpawnStop = 10;
    public GameObject monster;
    public GameObject monster2;

    bool swi = true;
    bool swi2 = true;


    void Start()
    {
        StartCoroutine("RandomSpawn");
        Invoke("Stop", SpawnStop);
    }

    //코루틴으로 랜덤하게 생성하기

    IEnumerator RandomSpawn()
    {
        while (swi)
        {
            //1초마다
            yield return new WaitForSeconds(StartTime);
            float y = Random.Range(ss, es);
            Vector2 r = new Vector2(transform.position.x, y);
            //몬스터 생성
            Instantiate(monster, r, Quaternion.identity);
        }
    }

    IEnumerator RandomSpawn2()
    {
        while (swi2)
        {
            //1초마다
            yield return new WaitForSeconds(StartTime + 2);
            float y = Random.Range(ss, es);
            Vector2 r = new Vector2(transform.position.x, y);
            //몬스터 생성
            Instantiate(monster2, r, Quaternion.identity);
        }
    }
    void Stop()
    {
        swi = false;
        StopCoroutine("RandomSpawn");

        StartCoroutine("RandomSpawn2");

        Invoke("Stop2", SpawnStop + 20);

    }

}

