using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("# Game Control")]
    // Header : 인스펙터의 속성들을 예쁘게 구분시켜주는 타이틀
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

    void Awake()
    {
        instance = this;
        // Awake 생명주기에서 인스턴스 변수를 자기자신 this로 초기화
    }

    void Start()
    {
        health = maxHealth;
        // 시작할 때 현재 체력과 최대 체력이 같도록 로직 추가
    }


    void Update()
    {
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

        if(exp == nextExp[level]) // 조건으로 필요 경험치에 도달하면 레벨 업하도록 구성
        {
            level++;
            exp = 0;

        }
    }

}
