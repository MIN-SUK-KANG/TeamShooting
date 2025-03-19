using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public GameObject target;

    bool isLive = true;

    Rigidbody2D rigid;
    SpriteRenderer spriter;


    public int Hp = 50;
    public int Attack = 3;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (!isLive)
            return;
        
        // "Player" 태그 오브젝트를 target으로 지정
        target = GameObject.FindGameObjectWithTag("Player");
        Debug.Log(target.transform.position);

        Vector2 dirVec = target.transform.position - transform.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.deltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        //rigid.linearVelocity = Vector2.zero;

    }

    private void LateUpdate()
    {
        if (!isLive)
            return;
        spriter.flipX = target.transform.position.x < transform.position.x;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
        }
    }



}
