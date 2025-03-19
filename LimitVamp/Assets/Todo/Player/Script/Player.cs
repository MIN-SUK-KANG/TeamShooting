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
    
    void Awake()
    {
        spriter = GetComponent<SpriteRenderer>(); //플레이어의 스프라이트랜더러 정보를 가져옴
        ani = GetComponent<Animator>();// 플레이어 이동 애니메이션을 가져옴
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
        return result;
    }
    
}
