using UnityEngine;
using UnityEngine.UI;

public class InGameOption : MonoBehaviour
{
    public GameObject UI_option;
    public GameObject UI_store;

    public Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }
    public void OptionOn()
    {
        UI_option.SetActive(true);
        Time.timeScale = 0;

    }

    public void OptionOff()
    {
        UI_option.SetActive(false);
        if (UI_store.activeInHierarchy == true)
        {
            Time.timeScale = 0;
            
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    //public void Retry()
    //{
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    //    Time.timeScale = 1f;
    //}
    public void Exit()
    {
        #if UNITY_EDITOR //에디터 테스트
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
