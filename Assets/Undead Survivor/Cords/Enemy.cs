using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // �ӵ�, ��ǥ, �������θ� ���� ���� ����
    public float speed;
    public float health;
    public float maxHealth;
    public RuntimeAnimatorController[] animCon;
    // ���ϸ������� �����ʹ� AnimatorController�� �����. ������ �ڵ忡���� RuntimeAnimatorController�� �����.
    public Rigidbody2D target;

    bool isLive;
    

    //������ٵ�2D�� ��������Ʈ�������� ���� ���� ����
    Rigidbody2D rigid;
    Animator anim;
    // �ִϸ����� ���� ���� �� �ʱ�ȭ�ϰ� ���� ���� �ۼ��ϱ�
    SpriteRenderer spriter;


    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        // ���Ͱ� ����ִ� ���ȿ��� �����̵��� ���� �߰�
        if(!isLive)
            return;

        Vector2 dirVec = target.position - rigid.position;
        // �½�Ʈ �� UnassignedReferenceException : The variable target of Enemy has not been assigned.
        // �����߻�
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

    // OnEnable : ��ũ��Ʈ�� Ȱ��ȭ �� ��, ȣ��Ǵ� �̺�Ʈ �Լ�
    void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        // ��ũ��Ʈ�� Ȱ��ȭ �� �� Ÿ���� �ʱ�ȭ �ϴ� �Լ�. 
        isLive = true;
        // OnEnable���� �������ο� ü�� �ʱ�ȭ
        health = maxHealth;
        // ������ ���� �ǻ�� ���� �� 0�̰ų� 0���� ���� ���� ������ �ȵǱ⿡ maxHealth�� �����
        // ������.
    }

    // �ʱ� �Ӽ��� �����ϴ� �Լ� �߰�
    public void Init(SpawnData data)  // �Ű������� ��ȯ������ �ϳ� ���� 
    {
        anim.runtimeAnimatorController = animCon[data.spriteType];
        // �Ű������� �Ӽ��� ���� �Ӽ� ���濡 Ȱ���ϱ�
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // OnTriggerEnter2D �Ű������� �±׸� �������� Ȱ��
        if (!collision.CompareTag("Bullet"))
            return;

        health -= collision.GetComponent<Bullet>().damage;
        // Bullet ������Ʈ�� �����Ͽ� �������� ������ �ǰ� ����ϱ�.
        // health��ü���� collision.GetComponent<Bullet>().damage�� �ش��ϴ� ���ڸ� �˾Ƽ� ����

        if(health > 0)// ���� ü���� �������� �ǰݰ� ������� ������ ������
        {
            // ���� �������. �ǰ� �׼�

        }
        else
        {
            // ���� ����
            Dead();
        }


    }

    void Dead()
    {
        // ����� �� SetActive �Լ��� ���� ������Ʈ ��Ȱ��ȭ. �׽�Ʈ��
        gameObject.SetActive(false);
    }


}
