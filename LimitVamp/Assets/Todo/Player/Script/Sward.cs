using UnityEngine;

public class Sward : MonoBehaviour
{
    public float speed = 100f;//회전 속도
    public GameObject arrow_center; // Empty Game Object
    public int damage =10;
    private void Start()
    {

    }
    void Update()
    {
        if (arrow_center != null) // 🔥 중심이 설정된 경우에만 회전
        {
            transform.RotateAround(arrow_center.transform.position, Vector3.forward, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemy enemyScript = collision.gameObject.GetComponent<enemy>();

            // enemy 스크립트가 존재하면 데미지를 줌
            if (enemyScript != null)
            {
                enemyScript.Damage(damage);
            }
        }
    }

}
