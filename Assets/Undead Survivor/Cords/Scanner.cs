using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    // ����, ���̾�, ��ĵ ��� �迭, ���� ����� ��ǥ�� ���� ���� ����
    public float scanRange;
    public LayerMask targetLayer;
    public RaycastHit2D[] targets;
    public Transform nearestTarget;

    void FixedUpdate()
    {
        targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, targetLayer);
        // CircleCastAll : ������ ĳ��Ʈ�� ��� ��� ����� ��ȯ�ϴ� �Լ�. �Ű������� �� ����
        // 1. ĳ���� ���� ��ġ
        // 2. ���� ������
        // 3. ĳ���� ����
        // 4. ĳ���� ����
        // 5. ��� ���̾�
        nearestTarget = GetNearest();
        // �ϼ��� �Լ�(GetNearest())�� ���� ���������� ���� ����� ��ǥ ������ ������Ʈ.
    }

    Transform GetNearest() //���� ����� ���� ã�� �Լ�
    {
        Transform result = null; //�ӽ� ���ϰ�
        float diff = 100; // Ž�� �Ÿ�?

        foreach (RaycastHit2D target in targets) // foreach������ ĳ���� ��� ������Ʈ�� �ϳ��� ����
        {
            Vector3 myPos = transform.position; // �÷��̾��� ��ġ
            Vector3 targetPos = target.transform.position; // ã�Ƴ� Ÿ���� ��ġ
            float curDiff = Vector3.Distance(myPos, targetPos);
            // Distance(A,B) : ���� A�� B�� �Ÿ��� ������ִ� �Լ�

            if (curDiff < diff)// �ݺ����� ���� ������ �Ÿ��� ����� �Ÿ����� ������ ��ü
            {
                diff = curDiff;
                result = target.transform;
            }

        }
        // �ϳ� �ϳ��� ���������� targets�� ���鼭 RaycastHit2D�� �ϳ��� ����. �װ��� target�� ��

        return result;
    }

}
