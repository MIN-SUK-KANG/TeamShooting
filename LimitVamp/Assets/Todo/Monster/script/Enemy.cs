using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float monsterHp;
    public float monsterMaxHp;

    public RuntimeAnimatorController[] animContrl;
    public GameObject target;

    bool isLive;

    Rigidbody2D rigid;
    Collider2D coll;
    Animator ani;
    SpriteRenderer spriter;
    WaitForFixedUpdate wait;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        ani = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
        wait = new WaitForFixedUpdate();
    }

    void FixedUpdate()
    {
        // GetCurrentAnimatorStateInfo -> Animation 현재 상태정보 확인
        if (!isLive || ani.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            return;
        
        Vector2 dirVec = target.transform.position - transform.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.deltaTime;
        rigid.MovePosition(rigid.position + nextVec);

    }

    private void LateUpdate()
    {
        if (!isLive)
            return;
        spriter.flipX = target.transform.position.x < transform.position.x;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet"))
            return;

        monsterHp -= collision.GetComponent<Bullet>().damage;
        StartCoroutine("KnockBack");

        if (monsterHp > 0)
        {
            ani.SetTrigger("Hit");
        }
        else
        {
            isLive = false;
            coll.enabled = false;       // Collider2D 비활성화
            rigid.simulated = false;    // Rigidbody2D 비활성화
            spriter.sortingOrder = 1;   // Order in Layer 조정
            ani.SetBool("Death", true);
        }


    }

    void OnEnable()
    {
        // "Player" 태그 오브젝트를 target으로 지정
        target = GameObject.FindGameObjectWithTag("Player");
        
        //기능 활성화
        isLive = true;
        coll.enabled = true;
        rigid.simulated = true;
        spriter.sortingOrder = 2;
        monsterHp = monsterMaxHp;
        ani.SetBool("Death", false);
    }

    public void Init(SpawnData data)
    {
        ani.runtimeAnimatorController = animContrl[data.spriteType];
        speed = data.speed;
        monsterMaxHp = data.monsterHp;
        monsterHp = data.monsterHp;
    }

    //넉백 코루틴 함수
    IEnumerator KnockBack()
    {
        yield return wait;
        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        Vector3 dirVec = transform.position - playerPosition;

        // 기존 속도 초기화 후 힘 적용
        rigid.linearVelocity = Vector2.zero;
        rigid.angularVelocity = 0f;

        rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
    }

    void Death()
    {
        gameObject.SetActive(false);
    }
}
