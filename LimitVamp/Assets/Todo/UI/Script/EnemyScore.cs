using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject a = GameObject.Find("UI_Score");
        UI_Score b = a.GetComponent<UI_Score>();

        b.curScore++;
        b.scoreUI.text = "now = " + b.curScore;

        if (b.curScore > b.highScore)
        {
            b.highScore = b.curScore;
            b.hscoreUI.text = "high = " + b.highScore;
        }
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
