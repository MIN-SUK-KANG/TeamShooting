using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Launcher : MonoBehaviour
{

    public GameObject Bullet; // 총알 프리팹을 담을 변수

    public Player player; // 플레이어 정보를 담을 변수

    public float Shooting_interval = 0.0f; // 총알 발사 간격 (초 단위), 인스펙터에서 설정
    private float prevInterval; // 이전 총알 속도를 저장하기 위한 변수

    private Coroutine shootingCoroutine; // 총알 발사 코루틴을 제어하기 위한 변수

    void Start()
    {
        shootingCoroutine = StartCoroutine(ShootContinuously()); // 총알 발사 코루틴 시작
        prevInterval = Shooting_interval; // 이전 총알 속도 초기화
    }

    void Update()
    {
        if(prevInterval != Shooting_interval) // 이전 총알 속도와 현재 총알 속도가 다른 경우
        {
            RestartShooting(); // 총알 발사 코루틴 재시작
            prevInterval = Shooting_interval; // 이전 총알 속도 갱신
        }
    }

    IEnumerator ShootContinuously()
    {
        while (true)
        {
            Shoot(); // 총알 발사
            yield return new WaitForSeconds(Shooting_interval); // Shooting_interval만큼 대기 후 반복
        }
    }

    void RestartShooting()
    {
        StopCoroutine(shootingCoroutine); // 기존 코루틴 중지
        shootingCoroutine = StartCoroutine(ShootContinuously()); // 새로운 속도로 재시작
    }

    void Shoot()
    {
        // 가장 가까운 타겟이 존재하는 경우에만 총알 발사
        if (player.nearestTarget != null)
        {
            Instantiate(Bullet, transform.position, Quaternion.identity); // 총알 생성
        }
    }

}