using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    public float speed = 4.0f;
    public int PlayerHp = 500;

    void Start()
    {
        
    }

    void Update()
    {
        float moveX = speed * Time.deltaTime * Input.GetAxisRaw("Horizontal");
        float moveY = speed * Time.deltaTime * Input.GetAxisRaw("Vertical");

        transform.Translate(moveX, moveY, 0);
    }
}
