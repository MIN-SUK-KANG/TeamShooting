using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Timer : MonoBehaviour
    {
        public GameObject Store = null;
        public Slider Bar_Time;
        public Player player;

        public Text round = null;
        public Text Score = null;
        public Text TimeShow = null;

        private int Nround = 0;
        private int Nscore = 0;

        private float[] RoundEndTime = { 10f, 30f, 60f };
        private float elapsedTime = 0.0f;

        bool shopDisplayed = false;

        void Update()
        {
            if (player != null)
            {
                if (elapsedTime > RoundEndTime.Last())
                {
                    elapsedTime = RoundEndTime.Last();
                    TimeShow.text = GetTime();
                }
                else
                {
                    elapsedTime += Time.deltaTime;
                    if (Nround <= 3)
                    {
                        if (elapsedTime >= RoundEndTime[Nround] && !shopDisplayed)
                        {
                            Store.SetActive(true);
                            shopDisplayed = true;

                            Time.timeScale = 0;
                            Nround += 1;
                        }

                        round.text = "Round: " + (Nround + 1);
                        if (Bar_Time != null)
                        {
                            if (Nround == 0)
                            {
                                Bar_Time.value = elapsedTime / RoundEndTime[0];
                            }
                            else
                            {
                                Bar_Time.value = (elapsedTime - RoundEndTime[Nround - 1]) / (RoundEndTime[Nround] - RoundEndTime[Nround - 1]);
                            }
                        }
                        TimeShow.text = GetTime();
                    }
                }


                Score.text = "Score: " + Nscore;
            }
        }

        public string GetTime()
        {
            string seconds = elapsedTime.ToString("F2");

            return seconds;
        }
        public int GetRound()
        {
            return Nround;
        }

        public void SetScore(int point)
        {
            Nscore += point;
        }

        public void CloseShop()
        {
            shopDisplayed = false;
        }

    }
}