using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public GameObject UI_option;

    public GameObject ABullet; // 자동 발사 총알 프리팹을 담을 변수
    public GameObject CBullet; // 클릭 발사 총알 프리팹을 담을 변수

    public Player player; // 플레이어 정보를 담을 변수

    public float bullet_speed = 0.0f; // 총알 발사 간격 (초 단위)
    private Coroutine shootingCoroutine; // 총알 발사 코루틴을 제어하기 위한 변수

    public float NeededTime = 0.1f; // 요구 경과 시간

    void Start()
    {
        shootingCoroutine = StartCoroutine(ShootContinuously()); // 총알 발사 코루틴 시작

    }

    void Update()
    {
        if (NeededTime > 0) // 요구 경과 시간이 남아있는 경우
        {
            NeededTime -= Time.deltaTime; // 경과 시간만큼 감소
        }


        if(Input.GetMouseButtonDown(0) && NeededTime <= 0) // 마우스 왼쪽 버튼 클릭 시
        {
            CShoot(); // 마우스 클릭 시 총알 발사
        }
    }

    IEnumerator ShootContinuously()
    {
        while (true)
        {
            AShoot(); // 총알 발사
            yield return new WaitForSeconds(bullet_speed); // bullet_speed만큼 대기 후 반복
        }
    }

    void RestartShooting()
    {
        StopCoroutine(shootingCoroutine); // 기존 코루틴 중지
        shootingCoroutine = StartCoroutine(ShootContinuously()); // 새로운 속도로 재시작
    }

    void AShoot()
    {
        // 가장 가까운 타겟이 존재하는 경우에만 총알 발사
        if (player.nearestTarget != null)
        {
            Instantiate(ABullet, transform.position, Quaternion.identity); // 총알 생성
            Debug.Log($"Bullet Spawn Position: {player.nearestTarget.position}"); // 총알 생성 위치 출력
        }
    }

    void CShoot()
    {
        if (UI_option.activeInHierarchy != true && Time.timeScale == 1) //게임이 진행중인 경우에만 발사
        {
            // 마우스 클릭 위치로 총알 발사
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float rotZ = Mathf.Atan2((mousePos - transform.position).y, (mousePos - transform.position).x) * Mathf.Rad2Deg;
            Instantiate(CBullet, transform.position, Quaternion.Euler(0, 0, rotZ)); // 총알 생성
            Debug.Log($"Bullet Spawn Position: {mousePos}"); // 총알 생성 위치 출력

            NeededTime = 0.1f; // 발사 후 요구 경과 시간 초기화
        }
    }
}