using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    [Header("# Buttons")]
    public Button btn_Start;
    public Button btn_Store;
    public Button btn_Collection;
    public Button btn_Option;
    public Button btn_Exit;
    //인스펙터에 할당할 버튼들
    public GameObject Title;

    [Header("# Groups")]
    public GameObject CharacterGroup;
    //인스펙터에서 활성, 비활성할 캐릭터 시작 부분(기존 메인화면을 활용)
    public GameObject Coll_ChararcterGroup;
    public GameObject Store;

    [Header("# Store item Choice")]
    public GameObject Health;
    public GameObject Attack;
    public GameObject Armor;
    public GameObject Speed;
    public GameObject Coin;


    //public GameObject BuyStatus;
    [Header("# Store item explanation")]
    public GameObject HealthEx;
    public GameObject AttackEx;
    public GameObject ArmorEx;
    public GameObject SpeedEx;
    public GameObject CoinEx;


    //public GameObject[] StatusChoices;


    public int HealthAct = 0;
    public int AttackAct = 0;
    public int ArmorAct = 0;
    public int SpeedAct = 0;
    public int CoinAct = 0;


    void Start()
    {
        //CharacterGroup.SetActive(false);
        //Coll_ChararcterGroup.SetActive(false);
        //Store.SetActive(false);
    }

    public void ButtonClick()//시작버튼 클릭
    {
        IPointerDownHandler();
    }

    public void Coll_ButtonClick()//컬렉션버튼 클릭
    {
        IPointerDownHandler();        
    }
    public void Store_ButtonClick()//상점버튼 클릭
    {
        Store_IPointerDownHandler();
        //switch()
        //{
        //    case 0:
        //        break;
        //}
    }
    

    public void IPointerDownHandler()//게임 시작페이지
    {
        //Debug.Log("시작");
        CharacterGroup.SetActive(true);
        Application.Quit();
        //아래는 해당 버튼말고 다른 버튼들은 초기화시킴. 타이틀하고
        btn_Start.gameObject.SetActive(false);
        btn_Store.gameObject.SetActive(false);
        btn_Collection.gameObject.SetActive(false);
        btn_Option.gameObject.SetActive(false);
    }

    public void Coll_IPointerDownHandler()//컬랙션
    {
        Coll_ChararcterGroup.SetActive(true);
        Application.Quit();
        //아래는 해당 버튼말고 다른 버튼들은 초기화시킴. 타이틀하고
        btn_Start.gameObject.SetActive(false);
        btn_Store.gameObject.SetActive(false);
        btn_Collection.gameObject.SetActive(false);
        btn_Option.gameObject.SetActive(false);
        Title.SetActive(false);
    }

    public void Store_IPointerDownHandler()// 상점
    {
        Store.SetActive(true);
        Application.Quit();
        //아래는 해당 버튼말고 다른 버튼들은 초기화시킴. 타이틀하고
        btn_Start.gameObject.SetActive(false);
        btn_Store.gameObject.SetActive(false);
        btn_Collection.gameObject.SetActive(false);
        btn_Option.gameObject.SetActive(false);
        Title.SetActive(false);
    }

    void DeactivateAllItems()
    {
        // 오브젝트가 활성화 상태 중 다른 오브젝트를 활성화 시킬 때 기존의 오브젝트를 비활성화 하기
        // 위해 필요함. 즉, 다른 오브젝트를 활성화시키면 그 전에 모든 오브젝트를 비활성화 시킴
        HealthEx.SetActive(false);
        AttackEx.SetActive(false);
        ArmorEx.SetActive(false);
        SpeedEx.SetActive(false);
        CoinEx.SetActive(false);
    }

    void buy()
    {
        if(HealthAct == 1)//체력 버튼이 활성화 됬을 때
        {

        }
    }

    public void HealthItemClick()
    {
        if(HealthEx.activeSelf)
        {
            HealthEx.SetActive(false);
            // 이미 활성화 되어 있으면 비활성화
        }
        else
        {
            DeactivateAllItems(); //모든 오브젝트 비활성화
            HealthEx.SetActive(true);
            //activeSelf를 이용해서 오브젝트가 활성화 상태인지 확인하고 그에 따라 활성화/비활성화를
            //토글 할 수 있게 함
            HealthAct = 1;
        }
        // 조건문으로 활성화 조건을 나눔
    }
    public void AttackItemClick()
    {
        if (AttackEx.activeSelf)
        {
            AttackEx.SetActive(false);            
        }
        else
        {
            DeactivateAllItems();
            AttackEx.SetActive(true);
            AttackAct = 1;
        }       
    }
    public void ArmorItemClick()
    {
        if (ArmorEx.activeSelf)
        {
            ArmorEx.SetActive(false);            
        }
        else
        {
            DeactivateAllItems();
            ArmorEx.SetActive(true);
            ArmorAct = 1;
        }
    }
    public void SpeedItemClick()
    {
        if (SpeedEx.activeSelf)
        {
            SpeedEx.SetActive(false);
        }
        else
        {
            DeactivateAllItems();
            SpeedEx.SetActive(true);
            SpeedAct = 1;
        }
    }
    public void CoinItemClick()
    {
        if (CoinEx.activeSelf)
        {
            CoinEx.SetActive(false);            
        }
        else
        {
            DeactivateAllItems();
            CoinEx.SetActive(true);
            CoinAct = 1;
        }
    }

    // 능력치 구입 시 OnCounts의 Count들이 활성화 되야 함.
 
}
