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

    public GameObject Gold;
    [SerializeField]
    private int gold = 10; // �� ����� ȹ�� ������ ���

    //������ٵ�2D�� ��������Ʈ�������� ���� ���� ����
    Rigidbody2D rigid;
    Collider2D coll;
    Animator anim;
    // �ִϸ����� ���� ���� �� �ʱ�ȭ�ϰ� ���� ���� �ۼ��ϱ�
    SpriteRenderer spriter;
    WaitForFixedUpdate wait;


    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
        wait = new WaitForFixedUpdate();
    }

    void FixedUpdate()
    {
        if (!GameManager.instance.isLive)
            return;

        // ���Ͱ� ����ִ� ���ȿ��� �����̵��� ���� �߰�
        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            // GetCurrentAnimatorStateInfo : ���� ���� ������ �������� �Լ�
            // IsName : �ش� ������ �̸��� ������ �Ͱ� ������ Ȯ���ϴ� �Լ�
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
        if (!GameManager.instance.isLive)
            return;

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
        coll.enabled = true;        
        rigid.simulated = true;        
        spriter.sortingOrder = 2;
        anim.SetBool("Dead", false);
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
        if (!collision.CompareTag("Bullet") || !isLive) 
            //��� ������ ���޾� ����Ǵ� ���� �����ϱ� ���� ���� �߰�
            return;

        health -= collision.GetComponent<Bullet>().damage;
        // Bullet ������Ʈ�� �����Ͽ� �������� ������ �ǰ� ����ϱ�.
        // health��ü���� collision.GetComponent<Bullet>().damage�� �ش��ϴ� ���ڸ� �˾Ƽ� ����

        StartCoroutine(KnockBack());
        // GetCurrentAnumatorStateInfo : ���� ���� ������ �������� �Լ�

        if(health > 0)// ���� ü���� �������� �ǰݰ� ������� ������ ������
        {
            // ���� �������. �ǰ� �׼�
            anim.SetTrigger("Hit");
            //�ǰ� �κп� �ִϸ������� SetTrigger �Լ��� ȣ���Ͽ� ���� ����
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Hit);
        }
        else// ���� ����
        {
            isLive = false;
            coll.enabled = false;
            // ������Ʈ�� ��Ȱ��ȭ�� .enabled = false
            rigid.simulated = false;
            // ������ٵ��� ������ ��Ȱ��ȭ�� .simulated = false
            spriter.sortingOrder = 1;
            anim.SetBool("Dead", true);
            GameManager.instance.kill++;
            GameManager.instance.GetExp();

            if (GameManager.instance.isLive)//�𵥵� ��� ����� ���� ���� �ÿ��� ���� �ʵ��� ���� �߰�. ���� �¸��� ��� ���Ͱ� �״µ� �̶� ��� ���尡 �Ѳ����� ��Ÿ���Ƿ� �÷��̾�� ū ������ ��ġ�� ����.
            {
                AudioManager.instance.PlaySfx(AudioManager.Sfx.Dead);
            }

            GameObject gold = Instantiate(Gold, transform.position, Quaternion.identity);
            //���Ͱ� ���� ��ġ�� ��� ����
            GameManager.instance.PlayerGold += 10;
            // ���Ӹ޴����� PlayerGold�� ��差�� ����������

        }


    }

    IEnumerator KnockBack()
    {
        yield return wait; // ���� ���� null�� �ϸ� 1������ ����
        // �ϳ��� ���� �������� ������ �����.
        //yield return new WaitForSeconds(2f);  // 2�� ����
        // yield return�� ���� �پ��� ���� �ð��� ����
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        // �÷��̾� ������ �ݴ� ���� : ���� ��ġ - �÷��̾� ��ġ
        rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
        // ������ٵ�2D�� AddForce �Լ��� �� ���ϱ�
        // �������� ���̹Ƿ� ForceMode2D.Impulse �Ӽ� �߰�
    }
    // �ڷ�ƾ(Coroutine) : ���� �ֱ�� �񵿱�ó�� ����Ǵ� �Լ�
    // IEnumerator : �ڷ�ƾ���� ��ȯ�� �������̽�
    // yield : �ڷ�ƾ�� ��ȯ Ű����

    void Dead()
    {
        // ����� �� SetActive �Լ��� ���� ������Ʈ ��Ȱ��ȭ. �׽�Ʈ��
        gameObject.SetActive(false);
    }


}
