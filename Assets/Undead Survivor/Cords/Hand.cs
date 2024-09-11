using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public bool isLeft;
    // ������, �޼� ������ ���� bool ���� ����
    public SpriteRenderer spriter;

    SpriteRenderer player;
    // �÷��̾��� ��������Ʈ������ ���� ���� �� �ʱ�ȭ

    Vector3 rightPos = new Vector3(0.35f, -0.15f, 0);
    Vector3 rightPosReverse = new Vector3(-0.35f, -0.15f, 0);
    Quaternion leftRot = Quaternion.Euler(0, 0, -25);
    // �޼��� �� ȸ���� Quaternion ���·� ����
    Quaternion leftRotReverse = Quaternion.Euler(0, 0, -125);


    void Awake()
    {
        player = GetComponentsInParent<SpriteRenderer>()[1];
    }

    void LateUpdate()
    {
        bool isReverse = player.flipX;
        // �÷��̾��� ���� ���¸� ���������� ����

        if (isLeft)// ��������
        {
            transform.localRotation = isReverse ? leftRotReverse : leftRot;
            // �޼� ȸ������ localRotation ���. �÷��̾� �������� ���ƾ� �ϱ� ����
            spriter.flipY = isReverse;
            // �޼� ��������Ʈ�� Y�� ����
            spriter.sortingOrder = isReverse ? 4 : 6;
            // �޼�, �������� sortingOrder�� �ٲٱ�
        }
        else// ���Ÿ�����
        {
            transform.localPosition = isReverse ? rightPosReverse : rightPos;
            spriter.flipX = isReverse;
            spriter.sortingOrder = isReverse ? 6 : 4;
        }


    }


}
