using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;

    public void Init(float damage)
    {
        this.damage = damage;
    }
}
