using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    void OnTriggerExit2D(Collider2D collision)
    {
        // OnTriggerExit2D의 매개변수 상대방 콜라이더의 태그를 조건으로.
        if (!collision.CompareTag("Area"))
            return;
        Vector3 playerPos = GameManager.instance.player.transform.position;
        // 거리를 구하기 위해 플레이어 위치와 타일맵 위치를 미리 저장
        Vector3 myPos = transform.position;
        // 나의 위치
        float diffX = Mathf.Abs(playerPos.x - myPos.x); //플레이어 위치 - 타일맵 위치 계산으로 거리 구하기
        // 무조건 양수가 되어야 함 즉, 절대값이 필요. 따라서 Mathf.Abs()함수를 사용함. 이것은 음수도 양수로 만들어주는 절대값 함수임
        float diffY = Mathf.Abs(playerPos.y - myPos.y);

        Vector3 playerDir = GameManager.instance.player.inputVec;
        // 플레이어의 이동 방향을 저장하기 위한 변수 추가
        float dirX = playerDir.x < 0 ? -1 : 1;
        float dirY = playerDir.y < 0 ? -1 : 1;

        switch (transform.tag)//실제 로직
        {
            case "Ground":
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
            case "Enemy":

                break;

        }
    }
}
