using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    Collider2D coll; // 모든 콜라이더의 기본 도형을 다 아우르는 클래스

    void Awake()
    {
        coll = GetComponent<Collider2D>();
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        // OnTriggerExit2D의 매개변수 상대방 콜라이더의 태그를 조건으로.
        if (!collision.CompareTag("Area"))
            return;
        Vector3 playerPos = GameManager.instance.player.transform.position;
        // 거리를 구하기 위해 플레이어 위치와 타일맵 위치를 미리 저장
        Vector3 myPos = transform.position;
        // 나의 위치
        //float diffX = Mathf.Abs(playerPos.x - myPos.x); //플레이어 위치 - 타일맵 위치 계산으로 거리 구하기
        //// 무조건 양수가 되어야 함 즉, 절대값이 필요. 따라서 Mathf.Abs()함수를 사용함. 이것은 음수도 양수로 만들어주는 절대값 함수임
        //float diffY = Mathf.Abs(playerPos.y - myPos.y);
        // => 스위치 안의 그라운드 케이스로 이동
        //Vector3 playerDir = GameManager.instance.player.inputVec;
        // 플레이어의 이동 방향을 저장하기 위한 변수 추가
        //float dirX = playerDir.x < 0 ? -1 : 1;
        //float dirY = playerDir.y < 0 ? -1 : 1;
        // =>> 캐릭터의 방향입력시 계산이 되는데 해당 로직이 급격한 이동으로 인한 오류가 발생        

        switch (transform.tag)//실제 로직
        {
            case "Ground":
                float diffX = playerPos.x - myPos.x;                                                                 
                float diffY = playerPos.y - myPos.y;
                float dirX = diffX < 0 ? -1 : 1;
                float dirY = diffY < 0 ? -1 : 1;
                diffX = Mathf.Abs(diffX);
                diffY = Mathf.Abs(diffY);
                // 플레이어와 타일맵의 거리 차이를 계산을 해서 마이너스이면 타일이 플레이어 보다 왼쪽에 있고, 플러스면 타일맵이 플레이어의 오론쪽에 있다. 이런식으로 변경
                // 두 오브젝트의 위치 차이를 활용한 로직으로 변경

                if (diffX > diffY)// 두 오브젝트의 거리차이에서 X축이 Y축보다 크면 수평이동
                {
                    transform.Translate(Vector3.right * dirX * 40);
                    // Translate : 지정된 값 만큼 현재 위치에서 이동
                    // Translate(Vector3.right * dirX * 40) 마지막에 40을 곱한 이유
                    // => 맵 크기가 20인데 맵을 이동 시 두 칸을 이동해야 하기에 2를 곱한 40을 곱함.
                }
                else if (diffX < diffY)
                {
                    transform.Translate(Vector3.up * dirY * 40);                    
                }
                break;
            case "Enemy": // 이곳에서 에너미 재배치 로직 사용
                if(coll.enabled) // 콜라이더가 활성화 되어있는지 조건 먼저 작성
                {
                    Vector3 dist = playerPos - myPos; //플레이어 위치 - 몬스터 위치
                    Vector3 ran = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), 0);
                    // 랜덤 벡터를 더하여 퍼져있는 몬스터 재배치 만들기
                    transform.Translate(ran + dist * 2);// <= 수정. 두 오브젝트의 거리를 그대로 활용하는 것이 포인트
                    //transform.Translate(playerDir * 20 + new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0f));
                    // 플레이어의 이동 방향에 따라 맞은 편에서 등장하도록 이동
                    // 랜덤한 위치에서 등장하도록 벡터 더하기
                }
                break;

        }
    }
}
