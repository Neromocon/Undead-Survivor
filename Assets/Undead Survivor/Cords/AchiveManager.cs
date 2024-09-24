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

    enum Achive { UnlockPotato, UnlockBean } // 업적 데이터와 같은 열거형 enum 작성
    Achive[] achives; // 업적 데이터들을 저장해둘 배열 선언 및 초기화
    WaitForSecondsRealtime wait;


    void Awake()
    {
        achives = (Achive[])Enum.GetValues(typeof(Achive));
        // Enum.GetValues : 주어진 열거형의 데이터를 모두 가져오는 함수
        // GetValues(typeof(Achive))에서 단순히 Achive를 쓸 수 가 없다.
        // 따라서 Achive가 어떤 타입이라는 것을 알려주기 위해 typeof()를 사용함.
        // Enum.GetValues 앞에 타입을 명시적으로 지정하여 타입 맞추기
        // => (Achive[])Enum.GetValues(typeof(Achive));
        wait = new WaitForSecondsRealtime(5); //5초 뒤 비활성화

        if (!PlayerPrefs.HasKey("MyData"))// HasKey 함수로 데이터 유무 체크 후 초기화 실행
        {
            Init();//데이터가 없으면 초기화를 시킴
        }



    }

    void Init() // 저장 데이터 초기화 함수 작성
    {
        PlayerPrefs.SetInt("MyData", 1);
        // PlayerPrefs : 간단한 저장 기능을 제공하는 유니티 제공 클래스
        // SetInt 함수를 사용하여 Key와 연결된 int형 데이터를 저장

        foreach (Achive achive in achives)
        // foreach를 활용하여 순차적으로 데이터 저장
        {
            //PlayerPrefs.SetInt("UnlockPotato", 0);
            // 아직 업적이 달성되지 않았기 때문에 업적 데이터와 동일한 이름의 Key로 0을 저장
            PlayerPrefs.SetInt(achive.ToString(), 0);
            // 이렇게 하면 일일이 코드를 작성할 필요가 없음
        }


    }

    void Start()
    {
        UnlockCharacter();
    }

    void UnlockCharacter()//캐릭터 버튼 해금을 위한 함수
    {
        for (int index = 0; index < lockCharacter.Length; index++)
        {
            // 잠금 버튼 배열을 순회하면서 인덱스에 해당하는 업적 이름 가져오기
            string achiveName = achives[index].ToString();
            bool isUnlock = PlayerPrefs.GetInt(achiveName) == 1;
            // GetInt 함수로 저장된 업적 상태를 가져와서 버튼 활성화에 적용
            lockCharacter[index].SetActive(!isUnlock);
            unlockCharacter[index].SetActive(isUnlock);

        }
    }

    void LateUpdate()
    {
        foreach(Achive achive in achives)
        // 모든 업적 확인을 위한 반복문을 LateUpdate에 작성
        {
            CheckAchive(achive);
        }
    }

    void CheckAchive(Achive achive)//업적 달성을 위한 함수
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

        if(isAchive && PlayerPrefs.GetInt(achive.ToString()) == 0) // 해당 업적이 처음 달성 했다는 조건을 if문에 작성
        {
            PlayerPrefs.SetInt(achive.ToString(), 1);

            for(int index = 0; index < uiNotice.transform.childCount; index++)
            {
                bool isActive = index == (int)achive;
                // 알림 창의 자식 오브젝트를 순회하면서 순번이 맞으면 활성화
                uiNotice.transform.GetChild(index).gameObject.SetActive(isActive);
            }

            StartCoroutine(NoticeRoutine());
        }


    }
    
    IEnumerator NoticeRoutine() // 알림 창을 활성화했다가 일정 시간 이후 비활성화하는 코루틴 생성
    {
        uiNotice.SetActive(true);//활성화 하고
        AudioManager.instance.PlaySfx(AudioManager.Sfx.LevelUp);


        yield return wait; // 5초를 기다리고

        uiNotice.SetActive(false);//비활성화
    }



}
