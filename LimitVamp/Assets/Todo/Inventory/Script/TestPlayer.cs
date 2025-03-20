using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    //스피드
    public float moveSpeed = 5f;


    void Start()
    {
        
    }

    void Update()
    {
        //방향키에따른 움직임
        float moveX = moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
        float moveY = moveSpeed * Time.deltaTime * Input.GetAxis("Vertical");

        transform.Translate(moveX, moveY, 0);
    }
}
