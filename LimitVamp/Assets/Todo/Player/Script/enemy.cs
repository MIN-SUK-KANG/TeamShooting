using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public float health;//적 체력
    public GameObject Item;//적이 떨굴 아이템
    public void Init(float health)
    {
        this.health = health;
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void Damage(int attack)//데미지함수
    {
        health -= attack; //적체력에 받는값만큼 깎음
        if (health <= 0) //체력이 0일때
        {
            ItemDrop();//아이템 드랍
            Destroy(gameObject);//적 제거
        }
    }

    public void ItemDrop()//아이템 드롭인데 중간중간 가끔 체력회복 정도 나오면 좋겠음
    {
        if (Item != null) // 드롭할 아이템이 존재하는지 확인
        {
            Instantiate(Item, transform.position, Quaternion.identity);//아이템 생성
        }
    }
}
