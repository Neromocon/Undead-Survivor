using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public static float Speed //�Լ��� �ƴ϶� �Ӽ����� �ۼ�
    {
        get { return GameManager.instance.playerId == 0 ? 1.1f : 1f; }
        // ���׿����ڸ� Ȱ���Ͽ� ĳ���Ϳ� ���� Ư��ġ ����
    }

    public static float WeaponSpeed //�Լ��� �ƴ϶� �Ӽ����� �ۼ�
    {
        get { return GameManager.instance.playerId == 1 ? 1.1f : 1f; }
        // ���׿����ڸ� Ȱ���Ͽ� ĳ���Ϳ� ���� Ư��ġ ����
    }

    public static float WeaponRate //�Լ��� �ƴ϶� �Ӽ����� �ۼ�
    {
        get { return GameManager.instance.playerId == 1 ? 0.9f : 1f; }
        // ���׿����ڸ� Ȱ���Ͽ� ĳ���Ϳ� ���� Ư��ġ ����
    }

    public static float Damage //�Լ��� �ƴ϶� �Ӽ����� �ۼ�
    {
        get { return GameManager.instance.playerId == 2 ? 1.2f : 1f; }
        // ���׿����ڸ� Ȱ���Ͽ� ĳ���Ϳ� ���� Ư��ġ ����
    }

    public static int Count //�Լ��� �ƴ϶� �Ӽ����� �ۼ�
    {
        get { return GameManager.instance.playerId == 3 ? 1 : 0; }
        // ���׿����ڸ� Ȱ���Ͽ� ĳ���Ϳ� ���� Ư��ġ ����
    }

}
