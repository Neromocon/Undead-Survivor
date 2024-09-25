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
    //�ν����Ϳ� �Ҵ��� ��ư��
    public GameObject Title;

    [Header("# Groups")]
    public GameObject CharacterGroup;
    //�ν����Ϳ��� Ȱ��, ��Ȱ���� ĳ���� ���� �κ�(���� ����ȭ���� Ȱ��)
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

    public void ButtonClick()//���۹�ư Ŭ��
    {
        IPointerDownHandler();
    }

    public void Coll_ButtonClick()//�÷��ǹ�ư Ŭ��
    {
        IPointerDownHandler();        
    }
    public void Store_ButtonClick()//������ư Ŭ��
    {
        Store_IPointerDownHandler();
        //switch()
        //{
        //    case 0:
        //        break;
        //}
    }
    

    public void IPointerDownHandler()//���� ����������
    {
        //Debug.Log("����");
        CharacterGroup.SetActive(true);
        Application.Quit();
        //�Ʒ��� �ش� ��ư���� �ٸ� ��ư���� �ʱ�ȭ��Ŵ. Ÿ��Ʋ�ϰ�
        btn_Start.gameObject.SetActive(false);
        btn_Store.gameObject.SetActive(false);
        btn_Collection.gameObject.SetActive(false);
        btn_Option.gameObject.SetActive(false);
    }

    public void Coll_IPointerDownHandler()//�÷���
    {
        Coll_ChararcterGroup.SetActive(true);
        Application.Quit();
        //�Ʒ��� �ش� ��ư���� �ٸ� ��ư���� �ʱ�ȭ��Ŵ. Ÿ��Ʋ�ϰ�
        btn_Start.gameObject.SetActive(false);
        btn_Store.gameObject.SetActive(false);
        btn_Collection.gameObject.SetActive(false);
        btn_Option.gameObject.SetActive(false);
        Title.SetActive(false);
    }

    public void Store_IPointerDownHandler()// ����
    {
        Store.SetActive(true);
        Application.Quit();
        //�Ʒ��� �ش� ��ư���� �ٸ� ��ư���� �ʱ�ȭ��Ŵ. Ÿ��Ʋ�ϰ�
        btn_Start.gameObject.SetActive(false);
        btn_Store.gameObject.SetActive(false);
        btn_Collection.gameObject.SetActive(false);
        btn_Option.gameObject.SetActive(false);
        Title.SetActive(false);
    }

    void DeactivateAllItems()
    {
        // ������Ʈ�� Ȱ��ȭ ���� �� �ٸ� ������Ʈ�� Ȱ��ȭ ��ų �� ������ ������Ʈ�� ��Ȱ��ȭ �ϱ�
        // ���� �ʿ���. ��, �ٸ� ������Ʈ�� Ȱ��ȭ��Ű�� �� ���� ��� ������Ʈ�� ��Ȱ��ȭ ��Ŵ
        HealthEx.SetActive(false);
        AttackEx.SetActive(false);
        ArmorEx.SetActive(false);
        SpeedEx.SetActive(false);
        CoinEx.SetActive(false);
    }

    void buy()
    {
        if(HealthAct == 1)//ü�� ��ư�� Ȱ��ȭ ���� ��
        {

        }
    }

    public void HealthItemClick()
    {
        if(HealthEx.activeSelf)
        {
            HealthEx.SetActive(false);
            // �̹� Ȱ��ȭ �Ǿ� ������ ��Ȱ��ȭ
        }
        else
        {
            DeactivateAllItems(); //��� ������Ʈ ��Ȱ��ȭ
            HealthEx.SetActive(true);
            //activeSelf�� �̿��ؼ� ������Ʈ�� Ȱ��ȭ �������� Ȯ���ϰ� �׿� ���� Ȱ��ȭ/��Ȱ��ȭ��
            //��� �� �� �ְ� ��
            HealthAct = 1;
        }
        // ���ǹ����� Ȱ��ȭ ������ ����
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

    // �ɷ�ġ ���� �� OnCounts�� Count���� Ȱ��ȭ �Ǿ� ��.
 
}
