using Unity.VisualScripting;
using UnityEngine;

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
    public int EXP = 100; //요구 경험치
    public int EXP_UP = 0;//현재 경험치
    public int level = 1;//레벨
    public float HP = 100;//체력
    private float damageCooldown = 0.1f; // 체력이 깎이는 간격 (1초)
    private float lastDamageTime; // 마지막으로 피해를 받은 시간
    private bool isTakingDamage = false; // 적과 충돌 여부
    private Color originalColor; // 원래 색상 저장
    public GameObject dead;//죽었을때 나오는 오브젝트


    void Awake()
    {
        spriter = GetComponent<SpriteRenderer>(); //플레이어의 스프라이트랜더러 정보를 가져옴
        ani = GetComponent<Animator>();// 플레이어 이동 애니메이션을 가져옴
        originalColor = spriter.color; // 원래 색상 저장
    }
    private void Start()
    {
        
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


        transform.Translate(moveX, moveY, 0); //이동

        //캐릭터의 월드 좌표를 뷰포트 좌표계로 변환해준다.
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        viewPos.x = Mathf.Clamp01(viewPos.x); //x값을 0이상, 1이하로 제한한다.
        viewPos.y = Mathf.Clamp01(viewPos.y); //y값을 0이상, 1이하로 제한한다.
        Vector3 worldPos = Camera.main.ViewportToWorldPoint(viewPos);//다시월드좌표로 변환
        transform.position = worldPos; //좌표를 적용한다.
        if(inputVec.x != 0) //마이너스쪽으로 갔을시 필립x를 적용시켜 반대방향으로 보이게 만듬
        {
            spriter.flipX = inputVec.x < 0;
        }
        ani.SetFloat("Speed", inputVec.magnitude); //스피드값이 늘어났을때 이동애니메이션이 실행되고 멈췄을때 멈춘 애니메이션 실행

        if (isTakingDamage && Time.time - lastDamageTime >= damageCooldown)
        {
            HP -= 1;
            lastDamageTime = Time.time;
            Debug.Log($"HP 감소! 현재 HP: {HP}");

            if (HP <= 0)
            {
                Debug.Log("플레이어 사망!");
                HP = 0;
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
    private void OnTriggerEnter2D(Collider2D collision)//아이템과 충돌했을때
    {
        if (collision.CompareTag("Item"))//태그가 아이템일시
        {
            Destroy(collision.gameObject);//부딪힌 아이템 삭제
            EXP_UP += 10;//현재 경험치 10증가
            if(EXP_UP == EXP)//현재경험치와 레벨업필요경험치가 같다면
            {
                EXP += 100;//레벨업 필요경험치 증가
                level++;//레벨 증가
                EXP_UP = 0;//현재 경험치 초기화
            }
            Debug.Log($"{EXP_UP}, {EXP}, {level}");//현재 경험치, 레벨업 필요경험치, 레벨 테스트출력
            //추후에 선택 후 선택 능력 증가로 변경 내의견으로는
            //근접무기강화(무기 개수증가), 원거리무기 강화(공속 증가), 
            //공격속도(근접무기 도는속도 증가), 공격력증가, 체력증가 정도?
            // 이중에서 하나 선택하면 그 능력이 강화로 해도 될듯합니다

        }
        if (collision.CompareTag("Enemy"))
        {
            isTakingDamage = true; // 적과 충돌 시작
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            isTakingDamage = false; // 적과 충돌 해제
        }
    }
}
