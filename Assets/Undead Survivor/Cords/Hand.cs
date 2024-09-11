using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public bool isLeft;
    // 오른손, 왼손 구분을 위한 bool 변수 선언
    public SpriteRenderer spriter;

    SpriteRenderer player;
    // 플레이어의 스프라이트렌더러 변수 선언 및 초기화

    Vector3 rightPos = new Vector3(0.35f, -0.15f, 0);
    Vector3 rightPosReverse = new Vector3(-0.35f, -0.15f, 0);
    Quaternion leftRot = Quaternion.Euler(0, 0, -25);
    // 왼손의 각 회전을 Quaternion 형태로 저장
    Quaternion leftRotReverse = Quaternion.Euler(0, 0, -125);


    void Awake()
    {
        player = GetComponentsInParent<SpriteRenderer>()[1];
    }

    void LateUpdate()
    {
        bool isReverse = player.flipX;
        // 플레이어의 반전 상태를 지역변수로 저장

        if (isLeft)// 근접무기
        {
            transform.localRotation = isReverse ? leftRotReverse : leftRot;
            // 왼손 회전에는 localRotation 사용. 플레이어 기준으로 돌아야 하기 때문
            spriter.flipY = isReverse;
            // 왼손 스프라이트는 Y축 반전
            spriter.sortingOrder = isReverse ? 4 : 6;
            // 왼손, 오른손의 sortingOrder를 바꾸기
        }
        else// 원거리무기
        {
            transform.localPosition = isReverse ? rightPosReverse : rightPos;
            spriter.flipX = isReverse;
            spriter.sortingOrder = isReverse ? 6 : 4;
        }


    }


}
