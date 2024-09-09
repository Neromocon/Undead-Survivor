using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // �������� ���� ���� ����
    public float damage;
    public int per;

    Rigidbody2D rigid; // �Ѿ� ��ũ��Ʈ���� ������ٵ�2D ���� ���� �� �ʱ�ȭ

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // ���� �ʱ�ȭ �Լ� ����
    public void Init(float damage, int per, Vector3 dir)
    {
        this.damage = damage;
        // this : �ش� Ŭ������ ������ ����
        this.per = per;

        if(per > -1) // ������ -1(����)���� ū �Ϳ� ���ؼ��� �ӵ� ����
        {
            rigid.velocity = dir * 15f; // velocity : �ӵ�
            // �ӷ��� �����־� �Ѿ��� ���ư��� �ӵ� ������Ű��
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") || per == -1) //���� ���� ������ if������ ���� �߰�
            return;

        per--;

        if(per == -1) // ���� ���� �ϳ��� �پ��鼭 -1�� �Ǹ� ��Ȱ��ȭ
        {
            rigid.velocity = Vector2.zero; // ��Ȱ��ȭ ������ �̸� ���� �ӵ� �ʱ�ȭ
            gameObject.SetActive(false);
        }

    }


}
