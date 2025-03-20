using UnityEngine;

public class Launcher : MonoBehaviour
{
    public GameObject bullet; //총알 프리팹 담을 변수
    public Player player;
    public float bullet_speed = 1.0f;
    void Start()
    {
        InvokeRepeating("Shoot", 3f, bullet_speed);//3초후 bullet_speed의 시간만큼 텀이있음
    }

    void Update()
    {
        
    }
    void Shoot()
    {
        if (player.nearestTarget != null)
        {
            Instantiate(bullet, transform.position, Quaternion.identity);//총알
            Debug.Log($"Bullet Spawn Position: {player.nearestTarget.position}");
        }
    }
}
