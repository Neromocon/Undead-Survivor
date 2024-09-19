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

        if (per >= 0) // ���Ÿ� ���⿡�� ������ ����
        {
            rigid.velocity = dir * 15f;            
        }
        //if(per > -1) // ������ -1(����)���� ū �Ϳ� ���ؼ��� �ӵ� ����
        //{
        //    rigid.velocity = dir * 15f; // velocity : �ӵ�
        //    // �ӷ��� �����־� �Ѿ��� ���ư��� �ӵ� ������Ű��
        //}
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") || per == -100) //���� ���� ������ if������ ���� �߰�
            //per == -1���� per == -100���� ����
            return;

        per--;

        //if (per == -1) // ���� ���� �ϳ��� �پ��鼭 -1�� �Ǹ� ��Ȱ��ȭ/ �Ʒ��� ���ǹ����� ����
        if (per < 0)//���� ������ ������ ���δ� if ������ �����ϰ� ����
        {
            rigid.velocity = Vector2.zero; // ��Ȱ��ȭ ������ �̸� ���� �ӵ� �ʱ�ȭ
            gameObject.SetActive(false);
        }

    }

    // ����ü�� ���� �̵��� ���� ����. OnTriggerExit2D�̺�Ʈ�� Area�� Ȱ���Ͽ� ���� ��Ȱ��ȭ
    void OnTriggerExit2D(Collider2D collision)
    {
        if(!collision.CompareTag("Area") || per == -100)
            return;//���������� ���

        gameObject.SetActive(false);//�÷��̾��� ������ ������ �̵��ϸ� ��Ȱ��ȭ ��Ŵ
    }



}
