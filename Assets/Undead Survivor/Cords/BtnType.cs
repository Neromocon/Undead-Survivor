using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BtnType : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
{
    public Btn_Type currentType;//이넘으로 만들어놓은 버튼 종류들을 선언
    public Transform buttonScale; //마우스를 가져다 대면 커지게 할 변수
    Vector3 defaultScale;

    void Start()
    {
        defaultScale = buttonScale.localScale;
    }
    public void OnBtnClick()// 버튼UI의 OnClick 이벤트에 연결할거임
    {
        switch (currentType)
        {
            case Btn_Type.New:
                Debug.Log("새게임");
                break;
            case Btn_Type.Store:
                Debug.Log("상점");
                break;
            case Btn_Type.Achivement:
                Debug.Log("상점");
                break;
            case Btn_Type.Option:
                Debug.Log("상점");
                break;
            case Btn_Type.Sound:
                Debug.Log("상점");
                break;
            case Btn_Type.Back:
                Debug.Log("상점");
                break;
            case Btn_Type.Quit:
                Debug.Log("상점");
                break;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)//버튼에 마우스가 닿으면 발생하는 이벤트
    {
        buttonScale.localScale = defaultScale * 1.2f;
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale;
    }
}
