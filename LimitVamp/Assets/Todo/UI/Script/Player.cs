using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour
{
    public float speed = 5.0f;
    public int hp = 20;
    int maxHP = 20;

    public Slider hpSlider;
    public GameObject hit;

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(moveX, moveY, 0);
        transform.Translate(speed * moveDirection *Time.deltaTime);
    }
    public void Damaged(int damage)
    {
        hp -= damage;
        hpSlider.value = (float)hp / (float)maxHP;

        if(hp > 0)
        {
            StartCoroutine(Hit());
        }
    }

    IEnumerator Hit()
    {
        hit.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        hit.SetActive(false);
    }
    
}
