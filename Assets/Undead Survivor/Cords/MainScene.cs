using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScene : MonoBehaviour
{
    // 시작,상점,컬렉션,옵션 버튼을 활용해야함. 버튼 활성화시 해당 기능이 작동해야힘
    // 시작 버튼 클릭 시 기존의 캐릭터 선택창이 떠야함.

    //bool isGameStart = false; // 게임 시작여부

    public Button btnStart;


    public void Awake()
    {
        btnStart.onClick.AddListener(OnClickStart);
    }
    
    public void OnClickStart()
    {
        Debug.Log("시작");
    }


    void Update()
    {
        
    }
}
