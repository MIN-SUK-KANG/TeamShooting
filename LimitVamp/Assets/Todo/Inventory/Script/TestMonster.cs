using UnityEngine;
using static Unity.VisualScripting.Member;

public class TestMonster : MonoBehaviour
{
    private float distance = 2f;
    private float spread = 10f;

    [SerializeField]
    private GameObject[] items; //아이템 오브젝트, 0 = 돈, 1 = 체력 회복, 2 = 파워업
    private System.Random rand = new System.Random();

    void Start()
    {

    }
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision) //플레이어와 몬스터가 접촉시 아이템 드랍하기
    {
        if (collision.gameObject.tag == "Player")
        {
            int first = rand.Next(1, 10);   //돈 1~9개 드랍
            int second = rand.Next(0, 5);  //체력 회복 0~4개 드랍
            int third = rand.Next(0, 4);   //파워업 0~3개 드랍

            int count = first + second + third;
            //드랍 아이템 사이의 각도
            float intervalAngle = 360 / count;

            for (int i = 0; i < count; i++)
            {
                float angle = i * intervalAngle;
                float radian = angle * Mathf.Deg2Rad;
                float x = Mathf.Cos(radian);
                float y = Mathf.Sin(radian);

                Vector2 itempos = new Vector2(x, y);
                Vector2 monsterpos = new Vector2(gameObject.GetComponent<Transform>().position.x, gameObject.GetComponent<Transform>().position.y);

                if (first > 0)
                {
                    GameObject item = Instantiate(items[0], (itempos * distance) + monsterpos, Quaternion.identity);
                    item.GetComponent<Rigidbody2D>().AddForce(itempos.normalized * spread);
                    first--;
                    continue;
                }
                if (second > 0)
                {
                    GameObject item = Instantiate(items[1], (itempos * distance) + monsterpos, Quaternion.identity);
                    item.GetComponent<Rigidbody2D>().AddForce(itempos.normalized * spread);
                    second--;
                    continue;
                }
                if (third > 0)
                {
                    GameObject item = Instantiate(items[2], (itempos * distance) + monsterpos, Quaternion.identity);
                    item.GetComponent<Rigidbody2D>().AddForce(itempos.normalized * spread);
                    third--;
                    continue;
                }
            }

        }

    }


}
