using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameOption : MonoBehaviour
{
    public GameObject UI_option;

    public void OptionOn()
    {
        UI_option.SetActive(true);
        Time.timeScale = 0;

    }

    public void OptionOff()
    {
        UI_option.SetActive(false);
        Time.timeScale = 1f;
    }

    //public void Retry()
    //{
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    //    Time.timeScale = 1f;
    //}
    public void Exit()
    {
        Application.Quit();
    }
}
