using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PoolManager pool;
    void Start()
    {
        instance = this;
    }

    void Update()
    {
        
    }
}
