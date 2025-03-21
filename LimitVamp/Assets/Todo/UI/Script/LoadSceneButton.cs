using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Button))]
    public class LoadSceneButton : MonoBehaviour
    {
        [SerializeField]
        string sceneName = string.Empty;

        SceneLoader sceneLoader;
        Button button;

        private void Awake()
        {
            sceneLoader = FindAnyObjectByType<SceneLoader>(); //컴포넌트에 없어도 씬로드 호출

            button = GetComponent<Button>();
            button.onClick.AddListener(OnButtonClick); //버튼 클릭 시 OnButtonClick 호출
        }

        private void OnButtonClick() // 버튼 클릭 시
        {
            if (sceneLoader != null) //SceneLoader 유무확인
            {
                sceneLoader.LoadScene(sceneName);
            }
            else
            {
                SceneManager.LoadScene(sceneName);
            }
        }
    }
}