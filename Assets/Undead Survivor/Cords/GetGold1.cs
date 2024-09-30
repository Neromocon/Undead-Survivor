using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetGold1 : MonoBehaviour
{
    public int GoldAmount = 10; //골드 증가량을 설정할 변수

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
            //플레이어와 접촉했는지 확인
        {
            // 코인 오브젝트 비활성화
            this.gameObject.SetActive(false);

            // HUD가 골드를 업데이트하도록 설정 (필요시 직접 호출)
            //HUD.instance.UpdateGoldUI(); // HUD 매니저에서 UI를 업데이트하는 함수 호출

            GameManager.instance.PlayerGold += GoldAmount;
            // 게임메니저의 PlayerGold에 골드량을 증가시켜줌
        }
    }
}
