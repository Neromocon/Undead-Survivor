using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // �ӵ�, ��ǥ, �������θ� ���� ���� ����
    public float speed;
    public Rigidbody2D target;

    bool isLive = true;
    

    //������ٵ�2D�� ��������Ʈ�������� ���� ���� ����
    Rigidbody2D rigid;
    SpriteRenderer spriter;


    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        // ���Ͱ� ����ִ� ���ȿ��� �����̵��� ���� �߰�
        if(!isLive)
            return;

        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        // ���� = ��ġ ������ ����ȭ(Normalized)
        // ��ġ ���� = Ÿ�� ��ġ - ���� ��ġ
        // �������� �������� ����� �޶����� �ʵ��� FixedDeltaTime ���.(�밢������ ������ �� �����¿� �̵� �ӵ��� �ٸ�)
        rigid.MovePosition(rigid.position + nextVec);
        // rigid.position ������ ��ġ + nextVec ������ ������ ��ġ.
        // �÷��̾��� Ű �Է� ���� ���� �̵� = ������ ���� ���� ���� �̵�
        rigid.velocity = Vector2.zero;
        // �浹 �� �ݴ�������� �з���. �̰��� ���ν�Ƽ�� �����ϱ� ����. �׷��� ���ν�Ƽ�� 0���� ������
        // ���� �ӵ��� �̵��� ������ ���� �ʵ��� �ӵ� ����

    }

    void LateUpdate()//�÷��̾� ��ġ�� ���� ���� ��ȯ
    {
        if (!isLive)
            return;

        spriter.flipX = target.position.x < rigid.position.x;
        // ��ǥ�� X�� ���� �ڽ��� X�� ���� ���Ͽ� ������ true�� �ǵ��� ����
    }


}
