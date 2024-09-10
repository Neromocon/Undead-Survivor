using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gear : MonoBehaviour
{
    public ItemData.ItemType type;
    public float rate;
    // ����� Ÿ�԰� ��ġ�� ������ ���� ����

    public void Init(ItemData data) // Weapon�� �����ϰ� �ʱ�ȭ �Լ� �ۼ�
    {
        // Basic Set
        name = "Gear" + data.itemId;
        transform.parent = GameManager.instance.player.transform;
        transform.localScale = Vector3.zero;

        // Property Set
        type = data.itemType;
        rate = data.damages[0];
        ApplyGear();
        // ApplyGear()�� ȣ���ϴ°� �����ε� ���� �ϴ���? �� �� ó�� ���� ���� ����.
        // �������� ���� �� �÷��̾ ������ �ִ� ��� ����鿡�� �� �� ���� ����� �� ������Ѿ� ��.

    }

    public void LevelUp(float rate)
    {
        this.rate = rate;
        ApplyGear();
        // ��� ���Ӱ� �߰��ǰų� ������ �� �� �������� �Լ��� ȣ��
    }


    // Ÿ�Կ� ���� �����ϰ� ������ ��������ִ� �Լ� �߰�
    void ApplyGear()
    {
        switch (type)
        {
            case ItemData.ItemType.Glove:
                RateUp();
                break;
            case ItemData.ItemType.Shoe:
                SpeedUp();
                break;
        }
    }



    void RateUp() // �尩�� ����� ������� �ø��� �Լ�
    {
        Weapon[] weapons = transform.parent.GetComponentsInChildren<Weapon>();
        // �÷��̾�� �ö󰡼� ��� Weapon�� ��������

        // foreach������ �ϳ��� ��ȸ�ϸ鼭 Ÿ�Կ� ���� �ӵ� �ø���
        foreach(Weapon weapon in weapons)
        {
            switch(weapon.id)
            {
                case 0:
                    weapon.speed = 150 + (150 * rate);
                    break;
                default :
                    weapon.speed = 0.5f * (1f - rate);
                    break;
            }
        }
    }

    // �Ź��� ����� �̵� �ӵ��� �ø��� �Լ� �ۼ�
    void SpeedUp()
    {
        float speed = 3;
        GameManager.instance.player.speed = speed + speed * rate;
    }


}
