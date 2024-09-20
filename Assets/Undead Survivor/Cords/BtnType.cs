using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BtnType : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
{
    public Btn_Type currentType;//�̳����� �������� ��ư �������� ����
    public Transform buttonScale; //���콺�� ������ ��� Ŀ���� �� ����
    Vector3 defaultScale;

    void Start()
    {
        defaultScale = buttonScale.localScale;
    }
    public void OnBtnClick()// ��ưUI�� OnClick �̺�Ʈ�� �����Ұ���
    {
        switch (currentType)
        {
            case Btn_Type.New:
                Debug.Log("������");
                break;
            case Btn_Type.Store:
                Debug.Log("����");
                break;
            case Btn_Type.Achivement:
                Debug.Log("����");
                break;
            case Btn_Type.Option:
                Debug.Log("����");
                break;
            case Btn_Type.Sound:
                Debug.Log("����");
                break;
            case Btn_Type.Back:
                Debug.Log("����");
                break;
            case Btn_Type.Quit:
                Debug.Log("����");
                break;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)//��ư�� ���콺�� ������ �߻��ϴ� �̺�Ʈ
    {
        buttonScale.localScale = defaultScale * 1.2f;
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale;
    }
}
