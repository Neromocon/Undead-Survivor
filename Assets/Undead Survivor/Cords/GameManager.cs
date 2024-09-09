using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float gameTime;
    public float maxGameTime = 2 * 10f;
    // 게임시간과 최대 게임시간을 담당할 변수 선언

    public PoolManager pool;
    // 다양한 곳에서 쉽게 접근할 수 있도록 게임매니저에 풀 매니저 추가
    public Player player;

    void Awake()
    {
        instance = this;
        // Awake 생명주기에서 인스턴스 변수를 자기자신 this로 초기화
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

}
