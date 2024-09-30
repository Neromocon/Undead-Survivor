using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetGold1 : MonoBehaviour
{
    public int GoldAmount = 10; //��� �������� ������ ����

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
            //�÷��̾�� �����ߴ��� Ȯ��
        {
            // ���� ������Ʈ ��Ȱ��ȭ
            this.gameObject.SetActive(false);

            // HUD�� ��带 ������Ʈ�ϵ��� ���� (�ʿ�� ���� ȣ��)
            //HUD.instance.UpdateGoldUI(); // HUD �Ŵ������� UI�� ������Ʈ�ϴ� �Լ� ȣ��

            GameManager.instance.PlayerGold += GoldAmount;
            // ���Ӹ޴����� PlayerGold�� ��差�� ����������
        }
    }
}
