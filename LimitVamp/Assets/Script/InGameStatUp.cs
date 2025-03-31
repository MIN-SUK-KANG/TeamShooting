using UI;
using UnityEditor;
using UnityEngine;
public class InGameStatUp : MonoBehaviour
{
    public Player player;
    public Launcher launcher;
    public Bullet bullet;

    public GameObject UI_store;
    private Timer timer;

    private void Awake()
    {
        timer = GameObject.Find("Timer").GetComponent<Timer>();
    }

    public void Speed_Up()//이동속도
    {
        player.moveSpeed += 1f;
        EditorUtility.SetDirty(player); // 인스펙터 강제 갱신
        Debug.Log("speedup");
    }
    public void Attack_Up()//공격력
    {
        bullet.damage += 2f;
        EditorUtility.SetDirty(bullet);
        Debug.Log("attackup");
    }
    public void Weppon_Speed_Up()//무기 공격속도
    {
        launcher.Shooting_interval -= 0.4f;
        if (launcher.Shooting_interval < 0.1f) launcher.Shooting_interval = 0.1f;
        EditorUtility.SetDirty(launcher);
        Debug.Log("wepponspeedup");
    }
    public void Player_HP()
    {
        player.HP += 10;
        EditorUtility.SetDirty(player);
        Debug.Log("HP");
    }
    public void StoreOff()
    {
        timer.CloseShop();
        UI_store.SetActive(false);
        Time.timeScale = 1f;
    }


}
