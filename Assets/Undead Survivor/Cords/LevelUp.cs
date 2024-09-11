using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    RectTransform rect;
    Item[] items;
    // 레벨업 스크립트에서 아이템 배열 변수 선언 및 초기화

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        items = GetComponentsInChildren<Item>(true);
    }

    public void Show()
    {
        Next();
        rect.localScale = Vector3.one;
        GameManager.instance.Stop();// 레벨 업 창이 나타나거나 사라지는 타이밍에 시간 제어
    }
    
    public void Hide()
    {
        rect.localScale = Vector3.zero;
        GameManager.instance.Resume();// 레벨 업 창이 나타나거나 사라지는 타이밍에 시간 제어
    }

    // 버튼을 대신 눌러주는 함수 작성
    public void Select(int index)
    {
        items[index].OnClick();
    }


    // 랜덤 아이템
    void Next()
    {
        // 1. 모든 아이템 비활성화
        foreach (Item item in items) // foreach를 활용하여 모든 아이템 오브젝트 비활성화
        {
            item.gameObject.SetActive(false);
        }

        // 2. 그 중에서 랜덤하게 3개 아이템만 활성화 하기
        int[] ran = new int[3]; // 랜덤으로 활성화 할 아이템의 인덱스 3개를 담을 배열 선언
        while(true)
        {
            ran[0] = Random.Range(0, items.Length);// 3개 데이터 모두 Random.Range 함수로 임의의 수 생성
            ran[1] = Random.Range(0, items.Length);
            ran[2] = Random.Range(0, items.Length);

            if (ran[0] != ran[1] && ran[1] != ran[2] && ran[0] != ran[2])
            // 서로 비교하여 모두 같지 않으면 반복문을 빠져나가도록 작성
                break;
        }

        for(int index = 0; index < ran.Length; index++)
        // for문을 통해 3개의 아이템 버튼 활성화
        {
            Item ranItem = items[ran[index]];
            // 3.만렙 이이템의 경우는 소비아이템으로 대체
            if(ranItem.level == ranItem.data.damages.Length)
            {
                items[4].gameObject.SetActive(true);
                //아이템이 최대 레벨이면 소비 아이템이 대신 활성화 되도록 작성
                // 소비 아이템이 2~3개와 같은 여러개일 경우
                // items[Random.Range(4, 7)].gameObject.SetActive(true);
                // 즉, 소비아이템 id가 4부터 7까지 중에서 랜덤으로 고름.
            }
            else
            {
                ranItem.gameObject.SetActive(true);
            }
            
        }

        




    }

}
