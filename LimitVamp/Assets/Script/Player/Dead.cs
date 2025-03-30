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
