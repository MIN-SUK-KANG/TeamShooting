using UnityEngine;

public class Bullet : MonoBehaviour
{
    // 적이 받을 피해량
    public int damage;

    // 퍼센트 (추가적인 보너스 데미지나 확률을 나타낼 수도 있음)
    public int per;

    // 총알 속도
    public float Speed = 4.0f;

    // 플레이어를 저장할 변수
    private GameObject target;

    // 가장 가까운 적의 Transform을 저장할 변수
    private Transform nearestTarget;

    // 마지막으로 향했던 방향을 저장하는 변수 (목표가 사라져도 유지)
    private Vector3 moveDirection;

    /// <summary>
    /// 총알의 데미지 및 확률을 초기화하는 메서드
    /// </summary>
    public void init(int damage, int per)
    {
        this.damage = damage;
        this.per = per;
    }

    void Start()
    {
        // "Player"라는 이름을 가진 오브젝트를 찾아서 target 변수에 저장
        target = GameObject.Find("Player");

        if (target != null) // 플레이어가 존재하는 경우
        {
            // 플레이어 스크립트(Player)를 가져옴
            Player player = target.GetComponent<Player>();

            // 플레이어가 존재하고, 가장 가까운 적(nearestTarget)이 있을 경우
            if (player != null && player.nearestTarget != null)
            {
                // 가장 가까운 적의 Transform을 저장
                nearestTarget = player.nearestTarget;

                // 총알이 처음 이동해야 할 방향을 설정
                moveDirection = (nearestTarget.position - transform.position).normalized;
            }
        }

        // 만약 이동 방향이 초기화되지 않았다면 (nearestTarget이 없었던 경우)
        if (moveDirection == Vector3.zero)
        {
            // 기본적으로 오른쪽(Vector3.right)으로 이동하도록 설정
            moveDirection = Vector3.right;
        }
    }

    void Update()
    {
        if (nearestTarget != null) // 목표(적)가 아직 존재하는 경우
        {
            // 지속적으로 방향을 업데이트하여 적을 따라감
            moveDirection = (nearestTarget.position - transform.position).normalized;
        }

        // 목표가 사라지더라도 마지막 방향으로 계속 이동
        transform.Translate(moveDirection * Speed * Time.deltaTime);
    }
    private void OnBecameInvisible()//화면 나가면 삭제
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌한 오브젝트가 "Enemy" 태그를 가지고 있다면
        if (collision.CompareTag("Enemy"))
        {
            // enemy 스크립트를 가져옴
            enemy enemyScript = collision.gameObject.GetComponent<enemy>();

            // enemy 스크립트가 존재하면 데미지를 줌
            if (enemyScript != null)
            {
                enemyScript.Damage(damage);
            }

            // 총알도 제거
            Destroy(gameObject);
        }
    }
}
