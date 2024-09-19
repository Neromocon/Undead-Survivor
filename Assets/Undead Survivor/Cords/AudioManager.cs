using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance; // �ڱ� �ڽ��� ���� �޸𸮿� �Ҵ�

    [Header("#BGM")]
    public AudioClip bgmClip;
    public float bgmVolume;
    AudioSource bgmPlayer;
    // ������� ���õ� Ŭ��, ����, ������ҽ� ���� ����
    AudioHighPassFilter bgmEffect; // Audio HighPass Filter �� ���� ���� ��

    [Header("#SFX")]//ȿ����
    public AudioClip[] sfxClips;
    public float sfxVolume;
    // �پ��� ȿ������ �� �� �ֵ��� ä�� ���� ���� ����
    public int channels;
    AudioSource[] sfxPlayers; // �پ��� ȿ������ ���ǹǷ� �ϳ��� �ƴ϶� �迭�� ������
    int channelIndex;

    public enum Sfx { Dead, Hit, LevelUp=3, Lose, Melee, Range=7, Select, Win }
    //������ �����ʹ� �����ϴ� ���ڸ� ������ �� ����
    // => LevelUp=3

    void Awake()
    {
        instance = this;
        Init();


    }

    void Init()
    {
        // ����� �÷��̾� �ʱ�ȭ
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform; // ������� ����ϴ� �ڽ� ������Ʈ�� ����
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        // AddComponent�Լ��� ������ҽ��� �����ϰ� ������ ����
        bgmPlayer.playOnAwake = false; // ĳ���͸� ���� �� ������� �߻��ؾ���
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;
        bgmPlayer.clip = bgmClip;
        bgmEffect = Camera.main.GetComponent<AudioHighPassFilter>();
        // ���� ī�޶� ������ Camera Ŭ������ ����ϸ� ����

        // ȿ���� �÷��̾� �ʱ�ȭ
        GameObject sfxObject = new GameObject("SfxPlayer");
        sfxObject.transform.parent = transform; // ȿ������ ����ϴ� �ڽ� ������Ʈ�� ����
        sfxPlayers = new AudioSource[channels]; // ä�� ���� ����Ͽ� ������ҽ� �迭 �ʱ�ȭ. �迭 ũ�⸸ �ʱ�ȭ ���� ��, ���빰�� �ʱ�ȭ�� �ȵ� ����

        for(int index = 0; index < sfxPlayers.Length; index++)
        {
            sfxPlayers[index] = sfxObject.AddComponent<AudioSource>();
            // �ݺ������� ��� ȿ���� ������ҽ� �����ϸ鼭 ����
            sfxPlayers[index].playOnAwake = false;
            sfxPlayers[index].bypassListenerEffects = true;
            // ȿ���� �ʱ�ȭ �ϴ� �κп� bypassListenerEffects�� true�� ����
            sfxPlayers[index].volume = sfxVolume;            
        }

    }

    public void PlayBgm(bool isPlay)//������� ����ϴ� �Լ�
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

    public void EffectBgm(bool isPlay)//������� ����ϴ� �Լ�
    {
        bgmEffect.enabled = isPlay;
    }

    public void PlaySfx(Sfx sfx) // ȿ���� ��� �Լ�
    {
        for(int index = 0;index < sfxPlayers.Length; index++)
        {
            int loopIndex = (index + channelIndex) % sfxPlayers.Length; //������ �Ѿ �� �ֱ� ������ sfxPlayers�� ���̸�ŭ ������
            // channelIndex : ���� �� �������� �����ߴ� �÷��̾��� �ε���
            // ä�� ������ŭ ��ȸ�ϵ��� ä���ε��� ���� Ȱ��

            if(sfxPlayers[loopIndex].isPlaying) //���� ����ǰ� �ִ��� ��� 
                continue;//����ǰ� ������ ���� ó���������� �Ѿ.

            int ranIndex = 0;
            if(sfx == Sfx.Hit || sfx == Sfx.Melee)
            {
                ranIndex = Random.Range(0, 2);//0���� ũ�Ⱑ 2�� ��������
                // ȿ������ 2�� �̻��� ���� ���� �ε����� ���ϱ�
                // => Hit�� 2���� Melee�� 3���� �ϴ� ��쿡�� ����ġ ������ ������ ��
            }

            channelIndex = loopIndex;
            sfxPlayers[loopIndex].clip = sfxClips[(int)sfx + ranIndex];
            //sfxPlayers[loopIndex].clip = sfxClips[(int)sfx];
            sfxPlayers[loopIndex].Play(); // ������ҽ��� Ŭ���� �����ϰ� Play �Լ� ȣ��
            break;// ȿ���� ����� �� ���Ŀ��� �� break�� �ݺ��� ����.
            // break �߿���!
        }

        

    }




}
