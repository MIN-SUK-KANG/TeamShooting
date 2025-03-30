using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("SFX")]
    public AudioClip[] sfxClips;
    public float sfxVolume;     //볼륨조절
    public int channels;        //동시에 재생될 수 있는 채널갯수
    AudioSource[] sfxPlayers;   //SFX 플레이어 배열
    int channelIndex;           //재생중인 채널의 index

    public enum Sfx { Hit, Death }; //Sfx Clip 사운드 목록 명명 : inspector 에서 등록

    void Awake()
    {
        instance = this;
        Init();
    }

    
    void Init()
    {
        //SFX 초기화
        GameObject sfxObject = new GameObject("sfxPlayer");
        sfxObject.transform.parent = transform; //AudioManager의 자식으로 생성
        sfxPlayers = new AudioSource[channels]; //채널 갯수만큼 배열 초기화
        
        //각 채널 데이터 초기화
        for (int i = 0; i < sfxPlayers.Length; i++)
        {
            sfxPlayers[i] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[i].playOnAwake = false;  //시작 시 효과음 바로 재생되지 않도록 설정 
            sfxPlayers[i].volume = sfxVolume;   //초기 볼륨 설정
        }
    }

    //sfx 재생
    public void PlaySfx(Sfx sfx)
    {
        // 채널갯수(channels) 만큼 생성 sfxPlayer 
        for (int i = 0; i < sfxPlayers.Length; i++)
        {
            int loopIndex = (i + channelIndex) % sfxPlayers.Length; //할당된 채널의 다음채널부터 할당하도록 index 보정
            
            if (sfxPlayers[loopIndex].isPlaying) //할당하려는 채널에 재생중인 sfx가 있다면 계속 재생 
                continue;

            channelIndex = loopIndex;       // 현재 재생할 채널 index를 channelIndex에 저장
            sfxPlayers[loopIndex].clip = sfxClips[(int)sfx];    //호출하는 클립으로 변경
            sfxPlayers[loopIndex].Play();                       //클립에 있는 소스 재생
            break;
        }
    }
}
