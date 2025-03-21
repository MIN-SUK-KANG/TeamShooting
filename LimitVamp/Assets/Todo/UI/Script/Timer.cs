using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Timer : MonoBehaviour
    {
        [SerializeField]
        Text Ntext = null;

        float elapsedTime = 0.0f;

        void Update()
        {
            elapsedTime += Time.deltaTime;

            Ntext.text = GetTime();
        }

        public string GetTime()
        {
            string minutes = Mathf.Floor(elapsedTime / 60).ToString("00");
            string seconds = (elapsedTime % 60).ToString("00");

            return minutes + ":" + seconds;
        }
    }
}