using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject[] prefabs;
    List<GameObject>[] pools;

    private void Awake()
    {
        //유니티 인스펙터에서 prefabs 배열에 목록 할당, 배열의 길이만큼 pools 내에 List 배열 생성
        pools = new List<GameObject>[prefabs.Length];

        for (int i = 0; i < pools.Length; i++)
        {
            //각 배열 위치에 빈 List 할당
            pools[i] = new List<GameObject>();
        }
    }

    public GameObject Get(int i)
    {
        GameObject select = null;

        //선택한 풀의 비활성화된 게임오브젝트 접근
        //pool[i]의 List는 특정 prefab의 사본들
        //즉, 특정 프리펩 = 특정 카테고리의 프리펩들
        //0이면 몬스터, 1이면 아이템 등...
        foreach (GameObject item in pools[i])
        {
            if (!item.activeSelf)
            {
                //발견 시 select 변수에 할당, 해당 게임오브젝트의 사본 활성화 취급
                select = item;
                select.SetActive(true);
                break;
            }
        }
        // 못찾은경우
        if (!select)
        {
            //새롭게 생성 하고 select 변수에 활당
            //즉 해당 카테고리에서 이번 플레이에 처음 등장한 오브젝트
            select = Instantiate(prefabs[i], transform);
            pools[i].Add(select);
        }
        //활성화된 게임오브젝트 반환
        return select;
        
    }
}
