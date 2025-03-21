using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameOption : MonoBehaviour
{
    public GameObject UI_option;
    private GameState state = GameState.Play; // state 변수를 GameState 타입으로 선언하고 초기화

    public enum GameState
    {
        Pause,
        Play
    }
    public void OptionOn()
    {
        UI_option.SetActive(true);
        Time.timeScale = 0;
        state = GameState.Pause;
    }

    public void OptionOff()
    {
        UI_option.SetActive(false);
        Time.timeScale = 1f;
        state = GameState.Play;
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }
    public void OnApplicationQuit()
    {
        Application.Quit();
    }

    void Update()
    {
        //플레이어 사망시. 애니메이션 정지. 버튼 활성화.
        //if(player.hp <= 0)
        //  {
        //      player.GetComponent<Animator>().SetFaloat("MoveMotion", 0);
        //      buttons.gameObject.SetActive(true);
        //  }   
    }
}
