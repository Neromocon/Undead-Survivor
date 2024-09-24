using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    public Button btn_Start;
    public Button btn_Store;
    public Button btn_Collection;
    public Button btn_Option;
    public Button btn_Exit;
    //�ν����Ϳ� �Ҵ��� ��ư��

    public GameObject Title;

    public GameObject CharacterGroup;
    //�ν����Ϳ��� Ȱ��, ��Ȱ���� ĳ���� ���� �κ�(���� ����ȭ���� Ȱ��)
    public GameObject Coll_ChararcterGroup;
    
    void Start()
    {
        CharacterGroup.SetActive(false);
        Coll_ChararcterGroup.SetActive(false);
    }

    public void ButtonClick()
    {
        IPointerDownHandler();
    }

    public void Coll_ButtonClick()
    {
        IPointerDownHandler();
        
    }


    public void IPointerDownHandler()
    {
        //Debug.Log("����");
        CharacterGroup.SetActive(true);
        Application.Quit();
        btn_Start.gameObject.SetActive(false);
        btn_Store.gameObject.SetActive(false);
        btn_Collection.gameObject.SetActive(false);
        btn_Option.gameObject.SetActive(false);
    }

    public void Coll_IPointerDownHandler()
    {
        Coll_ChararcterGroup.SetActive(true);
        Application.Quit();
        btn_Start.gameObject.SetActive(false);
        btn_Store.gameObject.SetActive(false);
        btn_Collection.gameObject.SetActive(false);
        btn_Option.gameObject.SetActive(false);
        Title.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
