using TMPro;
using UnityEngine;

//한글 주석 확인이 가능한지 체크용

public class TestScript : MonoBehaviour
{
    public TMP_Text Done;
    public Animator CoinControl;
    void Start()
    {
        CoinControl = GetComponent<Animator>();
        Done.text = "1번키: 하트\n2번키: 별\n3번키: 숫자\n현재 하트";
    }

    void Update()
    {
        if ( Input.GetKeyDown("1") || Input.GetKeyDown("[1]") )
        {
            Done.text = "1번키: 하트\n2번키: 별\n3번키: 숫자\n현재 하트";
            CoinControl.SetBool("1_heart", true);
            CoinControl.SetBool("2_star", false);
            CoinControl.SetBool("3_num", false);
        }
        if ( Input.GetKeyDown("2") || Input.GetKeyDown("[2]") )
        {
            Done.text = "1번키: 하트\n2번키: 별\n3번키: 숫자\n현재 별";
            CoinControl.SetBool("1_heart", false);
            CoinControl.SetBool("2_star", true);
            CoinControl.SetBool("3_num", false);
        }
        if ( Input.GetKeyDown("3") || Input.GetKeyDown("[3]") )
        {
            Done.text = "1번키: 하트\n2번키: 별\n3번키: 숫자\n현재 숫자";
            CoinControl.SetBool("1_heart", false);
            CoinControl.SetBool("2_star", false);
            CoinControl.SetBool("3_num", true);
        }
    }
}
