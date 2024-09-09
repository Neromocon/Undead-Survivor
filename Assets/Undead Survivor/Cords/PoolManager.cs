using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // ��������� ������ ������ �ʿ�
    public GameObject[] prefabs;
    // Ǯ ����� �ϴ� ����Ʈ���� �ʿ�
    List<GameObject>[] pools;
    // ������� ������ ������ 2���̸� ����Ʈ�� 2���� �ʿ���

    void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        for (int index = 0; index < pools.Length; index++) 
        {
            pools[index] = new List<GameObject>();
        }

        
    }
    // ���� ������Ʈ�� ��ȯ�ϴ� �Լ� ����
    public GameObject Get(int index)
    {
        GameObject select = null;
        // ���ӿ�����Ʈ ���������� ���� �̸� ����

        // ������ Ǯ�� ��� �ִ�(��Ȱ��ȭ ��) ���ӿ�����Ʈ ����.
        // �߰��ϸ� select ������ �Ҵ�
        foreach (GameObject item in pools[index])
        {
            // ���빰 ������Ʈ�� ��Ȱ��ȭ(��� ����)���� Ȯ��
            if(!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }
        // �� ã������ ���Ӱ� �����ϰ� select ������ �Ҵ�
        if(!select)
        {
            select = Instantiate(prefabs[index], transform);
            // Instantiate : ���� ������Ʈ�� �����Ͽ� ��鿡 �����ϴ� �Լ�
            pools[index].Add(select);
            // ������ ������Ʈ�� �ش� ������Ʈ Ǯ ����Ʈ�� �߰�
        }

        return select;
    }


}
