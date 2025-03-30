using System.Collections;
using UI;
using UnityEditor;
using UnityEngine;


//Instanciate를 몬스터에 집어넣어 아이템 오브젝트 인스턴스 생성하기
public class Item : MonoBehaviour
{
    private int itemData;  //아이템 데이터.
                           //기존에는 (string, int)로 string은 아이템의 종류, int는 아이템의 가치를 넣었지만
                           //드랍 아이템을 코인만 사용하기로 했으므로 int만 쓰도록 변경

    private bool isFlickerEnabled = false; //깜빡임 효과 활성화 여부
    private float time = 0f;

    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private AudioSource source;
    [SerializeField] private Timer timer;


    void Start()
    {
        sprite = GetComponent<SpriteRenderer>(); //아이템 생성시 해당 아이템 외형 가져오기
        source = GetComponent<AudioSource>(); //아이템 획득시 효과음
        timer = GameObject.Find("Timer").GetComponent<Timer>(); //타이머 스크립트 가져오기

        SetItemData(); //아이템의 종류와 현재 라운드에 따라 가치 설정. 5*(라운드+1)로. 1라운드면 개당 10, 2라운드면 개당 15, 3라운드면 개당 20.
    }
    private void Update()
    {
        this.time += Time.deltaTime;
        if (this.time > 10f) //아이템 드랍 후 10초 지나면 깜빡임 효과 활성화
        {
            isFlickerEnabled = true;
        }
        if (isFlickerEnabled)
        {
            StartCoroutine(FlickeringDestroy());
        }
    }


    private void OnCollisionEnter2D(Collision2D collision) //플레이어와 접촉시 아이템 획득, 사운드 재생, 아이템 오브젝트 제거
    {
        if (collision.gameObject.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(source.clip, transform.position);   //1회 재생하는 음악 인스턴스 생성
            timer.SetScore(itemData); //아이템 가치만큼 점수 증가
            Destroy(gameObject);
        }

    }
    private void SetItemData()
    {
        itemData = 5 * (timer.GetRound() + 2); //아이템 가치 설정. 라운드 표기는 1부터 시작하나 스크립트에서는 0부터 시작하므로 +2.
    }
    public int ReturnItemData()
    {
        return itemData;
    }



    IEnumerator FlickeringDestroy()
    {
        int i = 10;
        while (i-- > 0) //깜빡이 시작되면 4초에 걸쳐 10번 깜빡이다 사라짐
        {
            sprite.color = new Color(0, 0, 0, 0); //아이템 투명화
            yield return new WaitForSeconds(0.2f);
            sprite.color = new Color(255, 255, 255, 1); //아이템 색 복구
            yield return new WaitForSeconds(0.2f);
        }
        Destroy(gameObject);
    }



}
