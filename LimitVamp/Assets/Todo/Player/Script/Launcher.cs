using System;
using System.Collections;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public GameObject bullet; // 총알 프리팹을 담을 변수
    public Player player; // 플레이어 정보를 담을 변수
    public float bullet_speed = 1.0f; // 총알 발사 간격 (초 단위)
    private Coroutine shootingCoroutine; // 총알 발사 코루틴을 제어하기 위한 변수
    int now_level = 0; // 플레이어 레벨을 저장할 변수

    void Start()
    {
        now_level = player.level; // 플레이어의 초기 레벨 저장
        shootingCoroutine = StartCoroutine(ShootContinuously()); // 총알 발사 코루틴 시작
        
    }

    void Update()
    {
        // 플레이어의 레벨이 변경되었는지 확인
        if (now_level != player.level)
        {
            bullet_speed -= 0.2f; // 총알 발사 간격을 줄여서 속도 증가 -- 무기 레벨업 단계에 따라다르겠지만 수치는 정하면됨
            if (bullet_speed < 0.1f) bullet_speed = 0.1f; // 최소 속도 제한

            Console.WriteLine($": {now_level}, : {player.level}"); // 레벨 변화 로그 출력
            RestartShooting(); // 속도 변경 시 코루틴 다시 시작
            now_level = player.level;  // 이전 값 업데이트
        }
    }

    IEnumerator ShootContinuously()
    {
        while (true)
        {
            Shoot(); // 총알 발사
            yield return new WaitForSeconds(bullet_speed); // bullet_speed만큼 대기 후 반복
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
            Instantiate(bullet, transform.position, Quaternion.identity); // 총알 생성
            Debug.Log($"Bullet Spawn Position: {player.nearestTarget.position}"); // 총알 생성 위치 출력
        }
    }
}
