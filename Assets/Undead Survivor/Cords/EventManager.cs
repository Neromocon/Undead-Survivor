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
    //인스펙터에 할당할 버튼들

    public GameObject Title;

    public GameObject CharacterGroup;
    //인스펙터에서 활성, 비활성할 캐릭터 시작 부분(기존 메인화면을 활용)
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
        //Debug.Log("시작");
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
