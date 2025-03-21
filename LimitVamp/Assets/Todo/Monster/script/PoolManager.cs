using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject[] prefabs;

    //System.Collections.Generic
    List<GameObject>[] pools;

    private void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        for (int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }
    }

    public GameObject Get(int i)
    {
        GameObject select = null;

        //선택한 풀의 비활성화된 게임오브젝트 접근
        foreach(GameObject item in pools[i])
        {
            if (!item.activeSelf)
            {
                //발견 시 select 변수에 할당
                select = item;
                select.SetActive(true);
                break;
            }
        }
        // 못찾은경우
        if (!select)
        {
            //새롭게 생성 하고 select 변수에 활당
            select = Instantiate(prefabs[i], transform);
            pools[i].Add(select);
        }

        return select;
    }
}
