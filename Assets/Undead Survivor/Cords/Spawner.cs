using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    // 자식 오브젝트의 트랜스폼을 담을 배열 변수 선언
    public SpawnData[] spawnData;



    int level; // 소환 스크립트에서 레벨 담당 변수 선언
    float timer; //소환 타이머를 위한 변수 선언

    void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
        // GetComponentsInChildren 함수로 초기화. * GetComponentInChildren 와 별개의 함수임.
        // GetComponent != GetComponents 마지막 s자 주의!

    }


    void Update()
    {
        timer += Time.deltaTime;
        // 타이머 변수에는 deltaTime을 계속 더하기. deltaTime : 1프레임이 소비한 시간.
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 10f), spawnData.Length - 1);
        // 적절한 숫자로 나누어 시간에 맞춰 레벨이 올라가도록 작성
        // 그대로 진행하면 소수형으로 값이 나오는데 level은 정수 타입이라 오류가 발행함.
        // 따라서 Mathf함수의 FloorToInt를 사용함.
        // FloorToInt : 소수점 아래는 버리고 Int형으로 바꾸는 함수
        // CeilToInt : 소수점 아래를 올리고 Int형으로 바꾸는 함수 *참고
        // IndexOutOfRangeException : Index was outside the bounds of the array. 라는 오류 발생.
        // level의 크기를 넘어선 값이 되어서 발생한 오류로 추정. Mathf.Min추가
        // 인덱스 에러는 레벨 변수 계산 시 Min함수를 사용하여 막을 수 있음
        // Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 10f), spawnData.Length - 1)
        // 이렇게 하면 인덱스에서 위로 넘어가지 않기 때문에 에러 발생을 없앨 수 있음.
        if (timer > spawnData[level].spawnTime)// 타이머가 일정 시간 값에 도달하면 소환하도록 작성
        {
            timer = 0f;
            Spawn();
        }
        
    }

    void Spawn()// 소환 함수를 새로 작성
    {
        GameObject enemy = GameManager.instance.pool.Get(0);
        // 풀 함수에는 랜덤 인자 값을 넣도록 설정
        // instance 반환 값을 변수에 넣어두기
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        // 만들어둔  소환 위치 중 하나로 배치되도록 작성
        // 왜 랜덤 인자 값의 시작으 0이 아니라 1인가, 그것은 자기 자신도 포함하기 때문
        // 즉, 자식 오브젝트에서만 선택되도록 랜덤 시작은 1부터 해야함.
        enemy.GetComponent<Enemy>().Init(spawnData[level]);
        // 오브젝트 풀에서 가져온 오브젝트에서 Enemy 컴포넌트로 접근
        // 새롭게 작성한 함수를 호출하고 소환데이터 인자값 전달
    }

}

// ** 직렬화(Serialization) : 개체를 저장 혹은 전송하기 위해 변환
[System.Serializable] // 이렇게 하면 바로 아래의 개체는 직렬화가 됨.
// 직렬화를 하려면 직렬화를 할 개체 바로 위에 [System.Serializable]를 추가해야 함.
// []는 배열을 뜻하기도 하지만 속성을 뜻하기도 함.
// 직접 작성한 클래스를 직렬화를 통하여 인스펙터에서 초기화 가능
public class SpawnData // 소환 데이터를 담당하는 클래스 선언
{
    // 추가할 속성들 : 스프라이트 타입, 소환시간, 체력, 속도
    public float spawnTime;
    public int spriteType;
    public int health;
    public float speed;


}


