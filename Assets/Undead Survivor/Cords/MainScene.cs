using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScene : MonoBehaviour
{
    // ����,����,�÷���,�ɼ� ��ư�� Ȱ���ؾ���. ��ư Ȱ��ȭ�� �ش� ����� �۵��ؾ���
    // ���� ��ư Ŭ�� �� ������ ĳ���� ����â�� ������.

    //bool isGameStart = false; // ���� ���ۿ���

    public Button btnStart;


    public void Awake()
    {
        btnStart.onClick.AddListener(OnClickStart);
    }
    
    public void OnClickStart()
    {
        Debug.Log("����");
    }


    void Update()
    {
        
    }
}
