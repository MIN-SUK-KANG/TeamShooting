using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;


public class PlayOrder : MonoBehaviour
{
    public VideoPlayer first;
    public VideoPlayer second;
    private float timer = 0f;

    public string sceneName;

    void Start()
    {
        second.gameObject.SetActive(false);
        first.loopPointReached += OnFirstVideoEnd;
    }

    void OnFirstVideoEnd(VideoPlayer vp)
    {
        first.gameObject.SetActive(false);
        second.gameObject.SetActive(true);
        second.Play();
    }

    void Update()
    {
        if (!first.isPlaying)
        {
            timer += Time.deltaTime;
            if (timer >= 5f) // 5초가 지났을 때
            {
                if (Input.anyKeyDown)
                {
                    SceneManager.LoadScene("MainMenu");
                }
            }
        }
    }
}
