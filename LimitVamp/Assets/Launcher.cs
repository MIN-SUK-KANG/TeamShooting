using UnityEngine;

public class Launcher : MonoBehaviour
{
    public GameObject bullet; //총알 프리팹 담을 변수
    public Player player;
    void Start()
    {
        InvokeRepeating("Shoot", 3f, 0.5f);
    }

    void Update()
    {
        
    }
    void Shoot()
    {
        if (player.nearestTarget != null)
        {
            Instantiate(bullet, player.nearestTarget.position, Quaternion.identity);
            Debug.Log($"Bullet Spawn Position: {player.nearestTarget.position}");
        }
    }
}
