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

    void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1]; //�������̹Ƿ� �迭
        // �ڽ� ������Ʈ�� ������Ʈ�� �ʿ��ϹǷ� GetComponentsInChildren ���. s�� �߿���
        // GetComponentsInChildren���� �ι�° ������ �������� (ù��°�� �ڱ��ڽ��̹Ƿ�)
        icon.sprite = data.itemIcon;

        Text[] texts = GetComponentsInChildren<Text>();
        textLevel = texts[0];

    }

    // LateUpdate���� ���� �ؽ�Ʈ ���
    void LateUpdate()
    {
        textLevel.text = "Lv." + (level + 1);
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
