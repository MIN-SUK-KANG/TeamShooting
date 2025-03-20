using System.Collections;
using UnityEngine;


//Instanciate를 몬스터에 집어넣어 아이템 오브젝트 인스턴스 생성하기
public class Item : MonoBehaviour
{
    public int itemValue;  //아이템 가치, 돈이면 돈의 양, 체력 회복이면 회복량

    private bool isFlickerEnabled = false; //깜빡임 효과 활성화 여부
    private float time = 0f;

    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private AudioSource source;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>(); //아이템 생성시 해당 아이템 외형 가져오기
        SetItemValue(); //아이템의 종류와 현재 라운드에 따라 가치 설정... 현재는 무조건 10 할당하는 형태
        source = GetComponent<AudioSource>(); //아이템 획득시 효과음
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
            AudioSource.PlayClipAtPoint(source.clip, transform.position);
            Destroy(gameObject);
        }

        //플레이어에게 아이템의 값을 전달, 플레이어 스크립트 내에서 받아서 처리
        //itemValue를 public으로 넣었고, 아이템 각각의 카테고리는 따로 Tag로 넣었음. Player가 값이랑 카테고리를 받아와서 처리하면 될듯.

    }
    private void SetItemValue()
    {
        if(true) //일단 전부 10으로 세팅해두기
        {
            itemValue = 10;
        }


        //나중에 조건 분기 추가하기
        
        //else if (false)
        //{

        //}
        //else
        //{
        //    itemValue = 50;
        //}


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
