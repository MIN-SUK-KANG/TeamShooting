using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Timer : MonoBehaviour
    {
        public GameObject Store = null;
        public Slider Bar_Time;

        public Text round = null;
        public Text Ntext = null;

        private int Nround = 1;

        private float maxTime = 10f;
        public float elapsedTime = 0.0f;

        bool shopDisplayed = false;

        void Update()
        {
            elapsedTime += Time.deltaTime;

            Ntext.text = GetTime();
            if (elapsedTime >= maxTime && !shopDisplayed)
            {
                Store.SetActive(true);
                shopDisplayed = true;

                Time.timeScale = 0;
                Nround += 1;
            }

            round.text = "Round: " + Nround;

            if (Bar_Time != null)
            {
                Bar_Time.value = elapsedTime / maxTime;
            }
        }

        public string GetTime()
        {
            string seconds = elapsedTime.ToString("F2");

            return seconds;
        }
    }
}