using UnityEngine;

public class Slowzone : MonoBehaviour
{

    public float slowMultiplier = 0.5f;
    private float originalSpeed;


    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("진입");

        if (other.CompareTag("Player"))
        {
           
            Splayer player = other.GetComponent<Splayer>();

            if (player != null)
            {

                originalSpeed = player.moveSpeed;
                player.moveSpeed *= slowMultiplier;
            }
        }
    }        

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Splayer player = other.GetComponent<Splayer>();
            if (player != null)
            {
                player.moveSpeed = originalSpeed;
            }
        }
    }
}