using System.Threading;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;//적이 받는/캐릭터가주는 데미지
    public int per;//퍼센트
    public float Speed = 4.0f;

    public void init(int damage, int per)//변수 초기화
    {
        this.damage = damage;
        this.per = per;
    }
    void Start()
    {
        

    }

    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<enemy>().Damage(damage);
            Destroy(collision.gameObject);
            Destroy(gameObject);

        }
    }
}
