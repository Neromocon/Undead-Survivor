using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance; // 자기 자신을 정적 메모리에 할당

    [Header("#BGM")]
    public AudioClip bgmClip;
    public float bgmVolume;
    AudioSource bgmPlayer;
    // 배경음과 관련된 클립, 볼륨, 오디오소스 변수 선언
    AudioHighPassFilter bgmEffect; // Audio HighPass Filter 를 담을 변수 선

    [Header("#SFX")]//효과음
    public AudioClip[] sfxClips;
    public float sfxVolume;
    // 다양한 효과음을 낼 수 있도록 채널 개수 변수 선언
    public int channels;
    AudioSource[] sfxPlayers; // 다양한 효과음이 사용되므로 하나가 아니라 배열로 선언함
    int channelIndex;

    public enum Sfx { Dead, Hit, LevelUp=3, Lose, Melee, Range=7, Select, Win }
    //열거형 데이터는 대응하는 숫자를 지정할 수 있음
    // => LevelUp=3

    void Awake()
    {
        instance = this;
        Init();


    }

    void Init()
    {
        // 배경음 플레이어 초기화
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform; // 배경음을 담당하는 자식 오브젝트를 생성
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        // AddComponent함수로 오디오소스를 생성하고 변수에 저장
        bgmPlayer.playOnAwake = false; // 캐릭터를 누른 뒤 오디오가 발생해야함
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;
        bgmPlayer.clip = bgmClip;
        bgmEffect = Camera.main.GetComponent<AudioHighPassFilter>();
        // 메인 카메라 접근은 Camera 클래스를 사용하면 편함

        // 효과음 플레이어 초기화
        GameObject sfxObject = new GameObject("SfxPlayer");
        sfxObject.transform.parent = transform; // 효과음을 담당하는 자식 오브젝트를 생성
        sfxPlayers = new AudioSource[channels]; // 채널 값을 사용하여 오디오소스 배열 초기화. 배열 크기만 초기화 했음 즉, 내용물은 초기화가 안된 상태

        for(int index = 0; index < sfxPlayers.Length; index++)
        {
            sfxPlayers[index] = sfxObject.AddComponent<AudioSource>();
            // 반복문으로 모든 효과음 오디오소스 생성하면서 저장
            sfxPlayers[index].playOnAwake = false;
            sfxPlayers[index].bypassListenerEffects = true;
            // 효과음 초기화 하는 부분에 bypassListenerEffects를 true로 변경
            sfxPlayers[index].volume = sfxVolume;            
        }

    }

    public void PlayBgm(bool isPlay)//배경음을 재생하는 함수
    {
        if(isPlay)
        {
            bgmPlayer.Play();
        }
        else
        {
            bgmPlayer.Stop();
        }
    }

    public void EffectBgm(bool isPlay)//배경음을 재생하는 함수
    {
        bgmEffect.enabled = isPlay;
    }

    public void PlaySfx(Sfx sfx) // 효과음 재생 함수
    {
        for(int index = 0;index < sfxPlayers.Length; index++)
        {
            int loopIndex = (index + channelIndex) % sfxPlayers.Length; //범위를 넘어갈 수 있기 떄문에 sfxPlayers의 길이만큼 나눠줌
            // channelIndex : 지금 맨 마지막에 실행했던 플레이어의 인덱스
            // 채널 개수만큼 순회하도록 채널인덱스 변수 활용

            if(sfxPlayers[loopIndex].isPlaying) //지금 실행되고 있는지 물어봄 
                continue;//실행되고 있으면 다음 처리과정으로 넘어감.

            int ranIndex = 0;
            if(sfx == Sfx.Hit || sfx == Sfx.Melee)
            {
                ranIndex = Random.Range(0, 2);//0부터 크기가 2인 지점까지
                // 효과음이 2개 이상인 것은 랜덤 인덱스를 더하기
                // => Hit가 2개고 Melee이 3개다 하는 경우에는 스위치 문으로 나눠서 함
            }

            channelIndex = loopIndex;
            sfxPlayers[loopIndex].clip = sfxClips[(int)sfx + ranIndex];
            //sfxPlayers[loopIndex].clip = sfxClips[(int)sfx];
            sfxPlayers[loopIndex].Play(); // 오디오소스의 클립을 변경하고 Play 함수 호출
            break;// 효과음 재생이 된 이후에는 꼭 break로 반복문 종료.
            // break 중요함!
        }

        

    }




}
