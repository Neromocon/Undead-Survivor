using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemData data;
    public int level;
    public Weapon weapon;
    // ������ ������ �ʿ��� ������ ����
    public Gear gear; // ��ư ��ũ��Ʈ���� ���Ӱ� �ۼ��� ��� Ÿ���� ���� ����


    Image icon;
    Text textLevel;
    Text textName;
    Text textDesc;

    // ������ ��ũ��Ʈ�� �̸��� ���� �ؽ�Ʈ ���� �߰� �� �ʱ�ȭ

    void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1]; //�������̹Ƿ� �迭
        // �ڽ� ������Ʈ�� ������Ʈ�� �ʿ��ϹǷ� GetComponentsInChildren ���. s�� �߿���
        // GetComponentsInChildren���� �ι�° ������ �������� (ù��°�� �ڱ��ڽ��̹Ƿ�)
        icon.sprite = data.itemIcon;

        Text[] texts = GetComponentsInChildren<Text>();
        textLevel = texts[0];
        textName = texts[1];
        textDesc = texts[2];
        textName.text = data.itemName;

    }

    void OnEnable()
    {
        textLevel.text = "Lv." + (level + 1);

        switch (data.itemType)
        {
            case ItemData.ItemType.Melle:
            case ItemData.ItemType.Range:
                textDesc.text = string.Format(data.itemDesc, data.damages[level] * 100, data.counts[level]);
                break;
            case ItemData.ItemType.Glove:
            case ItemData.ItemType.Shoe:
                textDesc.text = string.Format(data.itemDesc, data.damages[level] * 100);
                break;
            default:
                textDesc.text = string.Format(data.itemDesc);
                break;

        }
        
    }



    // ��ư Ŭ�� �̺�Ʈ�� ������ �Լ� �߰�
    public void OnClick()
    {
        // ������ Ÿ���� ���� switch case �� �ۼ��صα�
        switch (data.itemType)
        {
            case ItemData.ItemType.Melle :
            case ItemData.ItemType.Range :
                if(level == 0)
                {
                    GameObject newWeapon = new GameObject();
                    // ���ο� ����������Ʈ�� �ڵ�� ����
                    weapon = newWeapon.AddComponent<Weapon>();
                    // AddComponent<T> : ���ӿ�����Ʈ�� T ������Ʈ�� �߰��ϴ� �Լ�
                    // AddComponent �Լ� ��ȯ ���� �̸� ������ ������ ����
                    weapon.Init(data);
                }
                else
                {
                    float nextDamage = data.baseDamage;
                    int nextCount = 0;

                    nextDamage += data.baseDamage * data.damages[level]; // ������ ���� ���� �� �������� ������. ������ �ǹ����� ������� �����Ͽ��� ������ ���̽� �������� ������.
                    // ó�� ������ �������� �������� Ƚ���� ���
                    nextCount += data.counts[level];

                    weapon.LevelUp(nextDamage, nextCount);
                    // Weapon Ŭ������ �ۼ��� ������ �Լ��� Ȱ��
                }
                level++;
                break;                            
            case ItemData.ItemType.Glove :
            case ItemData.ItemType.Shoe :
                if (level == 0)
                {
                    GameObject newGear = new GameObject();                    
                    gear = newGear.AddComponent<Gear>();
                    gear.Init(data);
                }
                else
                {
                    float nextRate = data.damages[level];
                    gear.LevelUp(nextRate);
                }
                level++;
                //���� ���� �ø��� ������ ����, ��� case �������� �̵�
                break;                
            case ItemData.ItemType.Heal :
                GameManager.instance.health = GameManager.instance.maxHealth;
                // ġ�� ����� ����� ������ case ������ �ٷ� �ۼ�
                break;

        }        

        if(level == data.damages.Length)// �ִ� ���� ������ �������� ����
        // ��ũ��Ʈ�� ������Ʈ�� �ۼ��� ���� ������ ������ �ѱ��� �ʰ� ���� �߰�
        {
            GetComponent<Button>().interactable = false;
        }
    }


}
