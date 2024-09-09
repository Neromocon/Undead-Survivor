using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // 프리펩들을 보관할 변수가 필요
    public GameObject[] prefabs;
    // 풀 담당을 하는 리스트들이 필요
    List<GameObject>[] pools;
    // 프리펩들 보관할 변수가 2개이면 리스트도 2개가 필요함

    void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        for (int index = 0; index < pools.Length; index++) 
        {
            pools[index] = new List<GameObject>();
        }

        
    }
    // 게임 오브젝트를 반환하는 함수 선언
    public GameObject Get(int index)
    {
        GameObject select = null;
        // 게임오브젝트 지역변수와 리턴 미리 설정

        // 선택한 풀의 놀고 있는(비활성화 된) 게임오브젝트 접근.
        // 발견하면 select 변수에 할당
        foreach (GameObject item in pools[index])
        {
            // 내용물 오브젝트가 비활성화(대기 상태)인지 확인
            if(!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }
        // 못 찾았으면 새롭게 생성하고 select 변수에 할당
        if(!select)
        {
            select = Instantiate(prefabs[index], transform);
            // Instantiate : 원본 오브젝트를 복제하여 장면에 생성하는 함수
            pools[index].Add(select);
            // 생성된 오브젝트는 해당 오브젝트 풀 리스트에 추가
        }

        return select;
    }


}
