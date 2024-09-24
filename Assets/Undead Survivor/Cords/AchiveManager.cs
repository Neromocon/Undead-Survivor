using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchiveManager : MonoBehaviour
{
    public GameObject[] lockCharacter;
    public GameObject[] unlockCharacter;
    public GameObject[] Coll_lockCharacter;
    public GameObject[] Coll_unlockCharacter;
    public GameObject uiNotice;

    enum Achive { UnlockPotato, UnlockBean } // ���� �����Ϳ� ���� ������ enum �ۼ�
    Achive[] achives; // ���� �����͵��� �����ص� �迭 ���� �� �ʱ�ȭ
    WaitForSecondsRealtime wait;


    void Awake()
    {
        achives = (Achive[])Enum.GetValues(typeof(Achive));
        // Enum.GetValues : �־��� �������� �����͸� ��� �������� �Լ�
        // GetValues(typeof(Achive))���� �ܼ��� Achive�� �� �� �� ����.
        // ���� Achive�� � Ÿ���̶�� ���� �˷��ֱ� ���� typeof()�� �����.
        // Enum.GetValues �տ� Ÿ���� ��������� �����Ͽ� Ÿ�� ���߱�
        // => (Achive[])Enum.GetValues(typeof(Achive));
        wait = new WaitForSecondsRealtime(5); //5�� �� ��Ȱ��ȭ

        if (!PlayerPrefs.HasKey("MyData"))// HasKey �Լ��� ������ ���� üũ �� �ʱ�ȭ ����
        {
            Init();//�����Ͱ� ������ �ʱ�ȭ�� ��Ŵ
        }



    }

    void Init() // ���� ������ �ʱ�ȭ �Լ� �ۼ�
    {
        PlayerPrefs.SetInt("MyData", 1);
        // PlayerPrefs : ������ ���� ����� �����ϴ� ����Ƽ ���� Ŭ����
        // SetInt �Լ��� ����Ͽ� Key�� ����� int�� �����͸� ����

        foreach (Achive achive in achives)
        // foreach�� Ȱ���Ͽ� ���������� ������ ����
        {
            //PlayerPrefs.SetInt("UnlockPotato", 0);
            // ���� ������ �޼����� �ʾұ� ������ ���� �����Ϳ� ������ �̸��� Key�� 0�� ����
            PlayerPrefs.SetInt(achive.ToString(), 0);
            // �̷��� �ϸ� ������ �ڵ带 �ۼ��� �ʿ䰡 ����
        }


    }

    void Start()
    {
        UnlockCharacter();
    }

    void UnlockCharacter()//ĳ���� ��ư �ر��� ���� �Լ�
    {
        for (int index = 0; index < lockCharacter.Length; index++)
        {
            // ��� ��ư �迭�� ��ȸ�ϸ鼭 �ε����� �ش��ϴ� ���� �̸� ��������
            string achiveName = achives[index].ToString();
            bool isUnlock = PlayerPrefs.GetInt(achiveName) == 1;
            // GetInt �Լ��� ����� ���� ���¸� �����ͼ� ��ư Ȱ��ȭ�� ����
            lockCharacter[index].SetActive(!isUnlock);
            unlockCharacter[index].SetActive(isUnlock);

        }
    }

    void LateUpdate()
    {
        foreach(Achive achive in achives)
        // ��� ���� Ȯ���� ���� �ݺ����� LateUpdate�� �ۼ�
        {
            CheckAchive(achive);
        }
    }

    void CheckAchive(Achive achive)//���� �޼��� ���� �Լ�
    {
        bool isAchive = false;

        switch (achive)
        {
            case Achive.UnlockPotato:
                isAchive = GameManager.instance.kill >= 10;
                break;
            case Achive.UnlockBean:
                isAchive = GameManager.instance.gameTime == GameManager.instance.maxGameTime;
                break;
        }

        if(isAchive && PlayerPrefs.GetInt(achive.ToString()) == 0) // �ش� ������ ó�� �޼� �ߴٴ� ������ if���� �ۼ�
        {
            PlayerPrefs.SetInt(achive.ToString(), 1);

            for(int index = 0; index < uiNotice.transform.childCount; index++)
            {
                bool isActive = index == (int)achive;
                // �˸� â�� �ڽ� ������Ʈ�� ��ȸ�ϸ鼭 ������ ������ Ȱ��ȭ
                uiNotice.transform.GetChild(index).gameObject.SetActive(isActive);
            }

            StartCoroutine(NoticeRoutine());
        }


    }
    
    IEnumerator NoticeRoutine() // �˸� â�� Ȱ��ȭ�ߴٰ� ���� �ð� ���� ��Ȱ��ȭ�ϴ� �ڷ�ƾ ����
    {
        uiNotice.SetActive(true);//Ȱ��ȭ �ϰ�
        AudioManager.instance.PlaySfx(AudioManager.Sfx.LevelUp);


        yield return wait; // 5�ʸ� ��ٸ���

        uiNotice.SetActive(false);//��Ȱ��ȭ
    }



}
