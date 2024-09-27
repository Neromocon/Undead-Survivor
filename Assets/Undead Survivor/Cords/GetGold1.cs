using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetGold1 : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.SetActive(false);
            GameManager.instance.PlayerGold += 10;
            // 게임메니저의 PlayerGold에 골드량을 증가시켜줌
        }
    }
}
