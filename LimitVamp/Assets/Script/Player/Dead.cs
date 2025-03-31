using System.Linq;
using UnityEngine;

public class Dead : MonoBehaviour
{
    public GameObject badEnding;
    public GameObject goodEnding;
    public GameObject player;

    void Start()
    {
    }
    void Update()
    {
        if (player == null)
        {
            BadEnding();
        }
        if (player != null && player.gameObject.GetComponent<Player>().getWin())
        {
            GoodEnding();
            GameObject.Find("SpawnManager").SetActive(false);
            GameObject.FindGameObjectsWithTag("Enemy").ToList().ForEach(x => x.SetActive(false));
        }
    }

    void BadEnding()
    {
        badEnding.SetActive(true);
    }
    void GoodEnding()
    {
        goodEnding.SetActive(true);
    }
}
