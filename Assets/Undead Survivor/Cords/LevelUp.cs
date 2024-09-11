using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    RectTransform rect;
    Item[] items;
    // ������ ��ũ��Ʈ���� ������ �迭 ���� ���� �� �ʱ�ȭ

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        items = GetComponentsInChildren<Item>(true);
    }

    public void Show()
    {
        Next();
        rect.localScale = Vector3.one;
        GameManager.instance.Stop();// ���� �� â�� ��Ÿ���ų� ������� Ÿ�ֿ̹� �ð� ����
    }
    
    public void Hide()
    {
        rect.localScale = Vector3.zero;
        GameManager.instance.Resume();// ���� �� â�� ��Ÿ���ų� ������� Ÿ�ֿ̹� �ð� ����
    }

    // ��ư�� ��� �����ִ� �Լ� �ۼ�
    public void Select(int index)
    {
        items[index].OnClick();
    }


    // ���� ������
    void Next()
    {
        // 1. ��� ������ ��Ȱ��ȭ
        foreach (Item item in items) // foreach�� Ȱ���Ͽ� ��� ������ ������Ʈ ��Ȱ��ȭ
        {
            item.gameObject.SetActive(false);
        }

        // 2. �� �߿��� �����ϰ� 3�� �����۸� Ȱ��ȭ �ϱ�
        int[] ran = new int[3]; // �������� Ȱ��ȭ �� �������� �ε��� 3���� ���� �迭 ����
        while(true)
        {
            ran[0] = Random.Range(0, items.Length);// 3�� ������ ��� Random.Range �Լ��� ������ �� ����
            ran[1] = Random.Range(0, items.Length);
            ran[2] = Random.Range(0, items.Length);

            if (ran[0] != ran[1] && ran[1] != ran[2] && ran[0] != ran[2])
            // ���� ���Ͽ� ��� ���� ������ �ݺ����� ������������ �ۼ�
                break;
        }

        for(int index = 0; index < ran.Length; index++)
        // for���� ���� 3���� ������ ��ư Ȱ��ȭ
        {
            Item ranItem = items[ran[index]];
            // 3.���� �������� ���� �Һ���������� ��ü
            if(ranItem.level == ranItem.data.damages.Length)
            {
                items[4].gameObject.SetActive(true);
                //�������� �ִ� �����̸� �Һ� �������� ��� Ȱ��ȭ �ǵ��� �ۼ�
                // �Һ� �������� 2~3���� ���� �������� ���
                // items[Random.Range(4, 7)].gameObject.SetActive(true);
                // ��, �Һ������ id�� 4���� 7���� �߿��� �������� ��.
            }
            else
            {
                ranItem.gameObject.SetActive(true);
            }
            
        }

        




    }

}
