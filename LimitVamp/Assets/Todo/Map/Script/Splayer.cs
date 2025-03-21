using UnityEngine;

public class Splayer : MonoBehaviour
{
    public float moveSpeed = 5f;



    private Vector2 minBounds = new Vector2(-11f, -11f);
    private Vector2 maxBounds = new Vector2(11f, 11f);


    void Start()
    {

    }


    void Update()
    {
        // 방향키 입력
        float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float moveY = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        // 이동 적용
        Vector3 newPosition = transform.position + new Vector3(moveX, moveY, 0);

        newPosition.x = Mathf.Clamp(newPosition.x, minBounds.x, maxBounds.x);
        newPosition.y = Mathf.Clamp(newPosition.y, minBounds.y, maxBounds.y);


        transform.position = newPosition;




    }
}
