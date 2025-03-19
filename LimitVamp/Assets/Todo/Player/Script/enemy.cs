using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public float health;
    public GameObject Item;
    public void Init(float health)
    {
        health = this.health;
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void Damage(int attack)
    {
        health -= attack;
        if (health <= 0)
        {
            ItemDrop();
            Destroy(gameObject);
        }
    }

    public void ItemDrop()
    {
        Instantiate(Item, transform.position, Quaternion.identity);
    }
}
