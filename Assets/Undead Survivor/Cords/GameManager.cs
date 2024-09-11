using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("# Game Control")]
    // Header : 인스펙터의 속성들을 예쁘게 구분시켜주는 타이틀
    public bool isLive;
    // 시간 정지 여부를 알려주는 bool 변수 선언
    public float gameTime;
    public float maxGameTime = 2 * 10f;
    // 게임시간과 최대 게임시간을 담당할 변수 선언
    [Header("# Player Info")]
    public int health;
    public int maxHealth = 100;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 };
    [Header("# Game Object")]
    public PoolManager pool;
    // 다양한 곳에서 쉽게 접근할 수 있도록 게임매니저에 풀 매니저 추가
    public Player player;
    public LevelUp uiLevelUp;



    void Awake()
    {
        instance = this;
        // Awake 생명주기에서 인스턴스 변수를 자기자신 this로 초기화
    }

    void Start()
    {
        health = maxHealth;
        // 시작할 때 현재 체력과 최대 체력이 같도록 로직 추가

        uiLevelUp.Select(0);
        //임시 스크립트 (첫번째 캐릭터 선택)
    }


    void Update()
    {
        if (!isLive)
            return;// 각 스크립트의 Update 계열 로직에 조건 추가하기

        gameTime += Time.deltaTime;
        // 타이머 변수에는 deltaTime을 계속 더하기. deltaTime : 1프레임이 소비한 시간.
        if (gameTime > maxGameTime)// 타이머가 일정 시간 값에 도달하면 소환하도록 작성
        {
            gameTime = maxGameTime;
            
        }

    }

    public void GetExp() // 경험치 증가 함수
    {
        exp++;

        if(exp == nextExp[Mathf.Min(level, nextExp.Length-1)]) // 조건으로 필요 경험치에 도달하면 레벨 업하도록 구성
        // 기존 nextExp[level]에서 Mathf를 활용하여 무한한 레벨업을 가능하게 함.
        // Min함수를 사용하여 최고 경험치를 그대로 사용하도록 변경
        {
            level++;
            exp = 0;
            uiLevelUp.Show();
            // 게임매니저의 레벨 업 로직에 창을 보여주는 함수 호출
        }
    }

    public void Stop()// 레벨업 시 시간 정지를 담당하는 함수
    {
        isLive = false;
        Time.timeScale = 0;
        // timeScale : 유니티의 시간 속도(배율)
    }


    public void Resume()// 능력 선택 후 다시 작동시키는 함수
    {
        isLive = true;
        Time.timeScale = 1;
    }


}
