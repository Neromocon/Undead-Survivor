using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    // 범위, 레이어, 스캔 결과 배열, 가장 가까운 목표를 담을 변수 선언
    public float scanRange;
    public LayerMask targetLayer;
    public RaycastHit2D[] targets;
    public Transform nearestTarget;

    void FixedUpdate()
    {
        targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, targetLayer);
        // CircleCastAll : 원형의 캐스트를 쏘고 모든 결과를 반환하는 함수. 매개변수가 좀 많음
        // 1. 캐스팅 시작 위치
        // 2. 원의 반지름
        // 3. 캐스팅 방향
        // 4. 캐스팅 길이
        // 5. 대상 레이어
        nearestTarget = GetNearest();
        // 완성된 함수(GetNearest())를 통해 지속적으로 가장 가까운 목표 변수를 업데이트.
    }

    Transform GetNearest() //가장 가까운 것을 찾는 함수
    {
        Transform result = null; //임시 리턴값
        float diff = 100; // 탐지 거리?

        foreach (RaycastHit2D target in targets) // foreach문으로 캐스팅 결과 오브젝트를 하나씩 접근
        {
            Vector3 myPos = transform.position; // 플레이어의 위치
            Vector3 targetPos = target.transform.position; // 찾아낸 타겟의 위치
            float curDiff = Vector3.Distance(myPos, targetPos);
            // Distance(A,B) : 백터 A와 B의 거리를 계산해주는 함수

            if (curDiff < diff)// 반복문을 돌며 가져온 거리가 저장된 거리보다 작으면 교체
            {
                diff = curDiff;
                result = target.transform;
            }

        }
        // 하나 하나씩 순차적으로 targets를 돌면서 RaycastHit2D를 하나씩 꺼냄. 그것이 target이 됨

        return result;
    }

}
