using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.UI;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Vector2 inputVec;
    public float moveSpeed = 5f; //플레이어 이동 속도

    public Animator ani; // 플레이어 자체 애니메이션
    SpriteRenderer spriter; // 플레이어 스프라이트

    public float scanRange;//주위스캔할 범위
    public LayerMask targetLayer; //레이어 비교대상
    public RaycastHit2D[] targets; //조건에 부합하는 대상들을 저장할 배열
    public Transform nearestTarget; //제일가까운 적의 좌표 저장변수

    public float HP = 100;//체력
    private float MHP;//최대체력
    public Slider HPBar;

    private float damageCooldown = 0.1f; // 체력이 깎이는 간격 (1초)
    private float lastDamageTime = 0f; // 마지막으로 피해를 받은 시간
    private bool isTakingDamage = false; // 적과 충돌 여부

    private Color originalColor; // 원래 색상 저장

    public GameObject dead;//죽었을때 나오는 오브젝트

    private bool winning = false;


    void Awake()
    {
        spriter = GetComponent<SpriteRenderer>(); //플레이어의 스프라이트랜더러 정보를 가져옴
        ani = GetComponent<Animator>();// 플레이어 이동 애니메이션을 가져옴
        originalColor = spriter.color; // 원래 색상 저장
        MHP = HP;
    }
    private void Start()
    {
        HPBar.value = 1;
    }

    void Update()
    {
        inputVec.x = Input.GetAxis("Horizontal"); //좌우 움직임
        inputVec.y = Input.GetAxis("Vertical");// 상하 움직임
        
    }
    private void FixedUpdate()
    {
        targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, targetLayer);//원형의 캐스트를 쏘고 모든 결과를 반환하는 함수
        nearestTarget = GetNearest();//가장가까운 적의 위치를 대입
        float moveX = moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal");//좌우 속도지정
        float moveY = moveSpeed * Time.deltaTime * Input.GetAxis("Vertical");//상하 속도 지정

        HPBar.value = HP / MHP;

        transform.Translate(moveX, moveY, 0); //이동

        //캐릭터의 월드 좌표를 뷰포트 좌표계로 변환해준다.
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        viewPos.x = Mathf.Clamp01(viewPos.x); //x값을 0이상, 1이하로 제한한다.
        viewPos.y = Mathf.Clamp01(viewPos.y); //y값을 0이상, 1이하로 제한한다.
        Vector3 worldPos = Camera.main.ViewportToWorldPoint(viewPos);//다시월드좌표로 변환
        transform.position = worldPos; //좌표를 적용한다.

        if (inputVec.y < 0) //아래로 이동할 경우
        {
            ani.SetBool("D", true);
            ani.SetBool("U", false);
        }
        else if (inputVec.y > 0) //위로 이동할 경우
        {
            ani.SetBool("D", false);
            ani.SetBool("U", true);
        }
        else //상하 이동 중이 아닐 경우
        {
            ani.SetBool("D", false);
            ani.SetBool("U", false);
        }

        if (inputVec.x < 0) //왼쪽으로 이동할 경우
        {
            ani.SetBool("R", false);
            ani.SetBool("L", true);
        }
        else if (inputVec.x > 0) //오른쪽으로 이동할 경우
        {
            ani.SetBool("R", true);
            ani.SetBool("L", false);
        }
        else //좌우 이동 중이 아닐 경우
        {
            ani.SetBool("R", false);
            ani.SetBool("L", false);
        }
        if (Mathf.Abs(inputVec.y) > Mathf.Abs(inputVec.x) ) //상하 이동 속도가 좌우 이동 속도보다 클 경우
        {
            ani.SetFloat("UDSpeed", inputVec.y); //상하 이동 속도
            ani.SetFloat("LRSpeed", 0); //좌우 이동 속도 0
            // 애니메이션에서 상하 이동만 적용
        }
        else //상하 이동 속도가 좌우 이동 속도보다 작거나 같을 경우
        {
            ani.SetFloat("UDSpeed", 0); //상하 이동 속도 0
            ani.SetFloat("LRSpeed", inputVec.x); //좌우 이동 속도
            // 애니메이션에서 좌우 이동만 적용
        }


        if (isTakingDamage && Time.time - lastDamageTime >= damageCooldown)
        {
            HP -= 1;
            lastDamageTime = Time.time;
            HPBar.value = HP / MHP;
            Debug.Log($"HP 감소! 현재 HP: {HP}");

            if (HP <= 0)
            {
                Debug.Log("플레이어 사망!");
                HP = 0;

                GameObject.Find("SpawnManager").SetActive(false);
                GameObject.FindGameObjectsWithTag("Enemy").ToList().ForEach(x => x.SetActive(false));

                Destroy(gameObject); // 플레이어 삭제
                Instantiate(dead, transform.position, Quaternion.identity); // 묘지 생성
            }
        }

        // 색상 변경 (적과 충돌 중일 때 점점 붉게 변함)
        if (isTakingDamage)
        {
            spriter.color = Color.Lerp(originalColor, Color.red, 0.5f); // 원래 색상에서 붉은색으로 50% 변경
        }
        else
        {
            spriter.color = originalColor; // 원래 색상으로 복구
        }
    }
    Transform GetNearest()//가장 가까운 것을 찾는 함수
    {
        Transform result = null;//반환할 변수 선언
        float diff = 100;//거리
        foreach(RaycastHit2D target in targets)//캐스팅 결과 오브젝트를 하나씩 접근
        {
            Vector3 myPos = transform.position; //플레이어 위치
            Vector3 targetPos = target.transform.position;//적의 위치
            float curDiff = Vector3.Distance(myPos, targetPos);//플레이어 위치와 적의 위치사의 거리를 계산하는 함수(Distance)
            if(curDiff < diff)//가져온 거리가 저장된 거리보다 작으면 교체하고 타겟의 위치를 반환할변수에 대입
            {
                diff = curDiff;
                result = target.transform;
            }
        }
        return result;//result담아 반환
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            isTakingDamage = true; // 적과 충돌 시작
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isTakingDamage = false; // 적과 충돌 해제
    }


    public void setWin()
    {
        winning = true;
    }
    public bool getWin()
    {
        return winning;
    }

}
