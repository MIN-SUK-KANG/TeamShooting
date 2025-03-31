using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UI;
using JetBrains.Annotations;

public class Enemy : MonoBehaviour
{
    //몬스터 능력치 및 상태
    private float speed, monsterHp, monsterMaxHp;
    bool isLive;

    //몬스터 오브젝트의 컴포넌트 목록
    Rigidbody2D rigid;
    CircleCollider2D coll;
    Animator ani;
    SpriteRenderer spriter;
    [SerializeField] private RuntimeAnimatorController[] animContrl;

    //플레이어 오브젝트
    public GameObject target;

    //드랍할 아이템 오브젝트 관련
    private System.Random rand = new System.Random();

    public Slider HPBar;

    [SerializeField] private GameObject item; //아이템 오브젝트. 코인.
    private float distance = 1f;
    private float spread = 20f;

    //스테이지 클리어
    public bool StageClearCheck = false;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<CircleCollider2D>();
        ani = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
    }
    void OnEnable()
    {
        // "Player" 태그 오브젝트를 target으로 지정
        target = GameObject.FindGameObjectWithTag("Player");
        //기능 활성화
        isLive = true;
        coll.enabled = true;
        rigid.simulated = true;
        monsterHp = monsterMaxHp;
        ani.SetBool("Death", false);
        HPBar.value = 1;
    }
    public void Init(SpawnData data)
    {
        //SpawnManager에서 정의된 클래스. 몬스터 형태 결정.
        //매 SpawnTime마다 생성, SpawnManager에서 체크.
        ani.runtimeAnimatorController = animContrl[data.spriteIndex];
        monsterMaxHp = monsterHp = data.monsterHp;
        speed = data.speed;

        //보스 여부 체크
        if (data.boss == true) StageClearCheck = true;

        //몬스터 크기에 따라 콜라이더 크기 조정
        if (spriter.size.x < spriter.size.y) coll.radius = spriter.size.x;
        else coll.radius = spriter.size.y;

    }




    void FixedUpdate()
    {
        // GetCurrentAnimatorStateInfo -> Animation 현재 상태정보 확인
        // 사망 혹은 피격 상태일 경우 일시 정지
        if (!isLive || ani.GetCurrentAnimatorStateInfo(0).IsName("Hit")) return;

        //그 외 이동
        if (target != null)
        {
            Vector2 dirVec = target.transform.position - transform.position;
            Vector2 nextVec = dirVec.normalized * speed * Time.deltaTime;
            rigid.MovePosition(rigid.position + nextVec);
        }

    }
    private void LateUpdate()
    {
        if (!isLive || target == null) return;
        //죽었으면 무시, 살아있다면 플레이어가 몬스터를 향해 바라보도록 설정
        spriter.flipX = target.transform.position.x < transform.position.x;
    }

    //넉백 코루틴 함수
    IEnumerator KnockBack()
    {
        yield return new WaitForFixedUpdate();
        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        Vector3 dirVec = transform.position - playerPosition;

        // 기존 속도 초기화 후 힘 적용
        rigid.linearVelocity = Vector2.zero;
        rigid.angularVelocity = 0f;

        rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
    }

    
    public void Damage(float damage)
    {
        //총알일 경우 피해를 입고, 보스가 아니면 넉백 코루틴 실행
        monsterHp -= damage;
        HPBar.value = monsterHp / monsterMaxHp;
        if (StageClearCheck == false) StartCoroutine("KnockBack");


        //피격 피해에 사망하지 않으면 피격 모션 실행
        if (monsterHp > 0)
        {
            ani.SetTrigger("Hit");
            //피격 사운드 추가
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Hit);
        }
        //몬스터 사망시 변수 처리, 물리연산 정지, 아이템 드랍 후 사망 모션 실행
        else
        {
            //사망 사운드 추가
            if(isLive)
                AudioManager.instance.PlaySfx(AudioManager.Sfx.Death);
            
            isLive = false;
            coll.enabled = false;
            rigid.simulated = false;
            ItemDrop();
            ani.SetBool("Death", true);
        }
    }
    public void ItemDrop()
    {
        int count = rand.Next(1, 5);   //코인 1~4개 드랍

        float intervalAngle = 360 / count;  //드랍 아이템 사이의 각도

        //원형으로 몬스터 주위에 아이템 드랍시키기
        for (int i = 0; i < count; i++)
        {
            float angle = i * intervalAngle + 90;
            float radian = angle * Mathf.Deg2Rad;
            float x = Mathf.Cos(radian);
            float y = Mathf.Sin(radian);

            Vector2 itempos = new Vector2(x, y);
            Vector2 monsterpos = new Vector2(gameObject.GetComponent<Transform>().position.x, gameObject.GetComponent<Transform>().position.y);

            if (count > 0)  //코인을 저장된 랜덤값, 즉 2~9개 생성.
            {
                GameObject todrop = Instantiate(item, (itempos * distance) + monsterpos, Quaternion.identity);
                todrop.GetComponent<Rigidbody2D>().AddForce(itempos.normalized * spread);
            }
        }
    }

    //내부 함수로 부르는게 아니라 유니티 애니메이터에서 사용하기 위해 public으로 선언
    void Death()
    {
        gameObject.SetActive(false);
        if(StageClearCheck == true)
            target.GetComponent<Player>().setWin();
    }
}
