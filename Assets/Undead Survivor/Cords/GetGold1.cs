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
            // ���Ӹ޴����� PlayerGold�� ��差�� ����������
        }
    }
}
