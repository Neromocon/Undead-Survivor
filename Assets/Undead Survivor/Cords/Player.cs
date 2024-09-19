using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;
    public Scanner scanner; // �÷��̾� ��ũ��Ʈ���� �˻� Ŭ���� Ÿ�� ���� ���� �� �ʱ�ȭ
    public Hand[] hands;
    public RuntimeAnimatorController[] animCon;
    // �÷��̾� ��ũ��Ʈ�� ���� �ִϸ����� ��Ʈ�ѷ��� ������ �迭 ���� ����

    Rigidbody2D rigid;
    // ���� ������Ʈ�� ������ٵ� 2D�� ������ ���� ����
    SpriteRenderer spriter;
    Animator anim;

    // Start is called before the first frame update
    void Awake()//������ �� �ѹ��� ����Ǵ� �����ֱ� 
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        // GetComponent<T> : ������Ʈ���� ������Ʈ�� �������� �Լ�. T�ڸ��� ������Ʈ �̸� �ۼ�
        anim = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
        hands = GetComponentsInChildren<Hand>(true);
        // �÷��̾�� �� ��ũ��Ʈ�� ���� �迭���� ���� �� �ʱ�ȭ
        // �Լ� ���� ���� �߰� ���� ���� �� �÷��̾� ������Ʈ�� ������� ��Ȱ��ȭ �Ǿ��� ������ ��󿡼� ���ܰ� ��
        // ���� ���ڰ��� true�� �����ϸ� Ȱ��ȭ ����
    }

    void OnEnable()
    {
        speed *= Character.Speed;
        anim.runtimeAnimatorController = animCon[GameManager.instance.playerId];
    }

    // Update is called once per frame
    // => Update : �ϳ��� �����Ӹ��� �ѹ��� ȣ��Ǵ� �����ֱ� �Լ�.
    //   -> Ex) 60�������̸� 1�ʿ� 60�� ������ �Ǵ� ����.
    void Update()
    {
        if (!GameManager.instance.isLive)
            return;

        //inputVec.x = Input.GetAxisRaw("Horizontal");
        // inputVec.x = Input.GetAxis("Horizontal");
        //inputVec.y = Input.GetAxisRaw("Vertical");
        // Input : ����Ƽ���� �޴� ��� �Է��� �����ϴ� Ŭ����.
        // GetAxis() : �࿡ ���� ��. � ���̶�� ����� ���ڿ��� ����.
        // ����Ƽ �����Ϳ��� Edit -> Project -> Input Manager���� ��ư�̸� Ȯ�� ����.
        // Input Manager�� �������� �Է��� ������ ��ư���� �����ϴ� ����.
        // Axis ���� -1���� 1����.

        // �����Ϳ��� �̵��� ���� �񽺹����Ѱ� �ִµ� �̰��� Input.GetAxis ����
        // Input.GetAxis�� �Է� ���� �ε巴�� �ٲ���.
        // GetAxisRaw�� ���� ��Ȯ�� ��Ʈ�� ���� ����

        //===============
        // Input Manager�� �ƴ� Input System���� ��ȯ��.



    }

    void FixedUpdate()
    //FixedUpdate : ���� ���� �����Ӹ��� ȣ��Ǵ� �����ֱ� �Լ�.
    {
        if (!GameManager.instance.isLive)
            return;

        Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;
        // normalized : ���� ���� ũ�Ⱑ 1�� �ǵ��� ��ǥ�� ������ ��.
        // ��Ÿ��� ���Ǹ� ������ ����. ���� 1�ȼ��� �����̰ų� ���������� 1�ȼ� �����̴� ���̶�
        // �ٸ��� ������ ���� �����̴� ���̴� �ٸ���.
        // fixedDeltaTime : ���� ������ �ϳ��� �Һ��� �ð�.
        // Input System���� ����ϱ⿡ normalized�� ��. �̹� �������� normalized�� �����Ŵ.

        // �����̵� ù��° : ���� �ش� == AddForce
        //rigid.AddForce(inputVec);

        // �����̵� �ι�° : �ӵ��� ���� �����Ѵ� == Velocity
        //rigid.velocity = inputVec;

        // �����̵� ����° : ��ġ�� �ű�� == MovePosition / ü������ �����̵���, �����̵� �ϵ���.
        rigid.MovePosition(rigid.position + nextVec);
        // MovePosition�� ��ġ �̵��̶� ���� ��ġ�� �����־�� ��.
        // ���� rigid.position�� ������.
        // �̴�� �ϸ� ������ ����. �����ӿ� ���� �ӵ��� ����
        // ���� �ٸ� ������ ȯ�濡���� �̵��Ÿ��� ���ƾ� ��.



    }

    

    void LateUpdate()
    // LateUpdate : �������� ���� �Ǳ� �� ����Ǵ� �����ֱ� �Լ�
    {
        if (!GameManager.instance.isLive)
            return;

        anim.SetFloat("Speed", inputVec.magnitude);
        // SetFloat ù��° ���� : �Ķ��Ÿ �̸�
        // SetFloat �ι�° ���� : ������ float ��
        // magnitude : ������ ������ ũ�� ��


        if (inputVec.x != 0)
        {
            spriter.flipX = inputVec.x < 0;
        }
    }

    void OnCollisionStay2D(Collision2D collision)//�ǰ� ����
    {
        if(!GameManager.instance.isLive)
            return;

        GameManager.instance.health -= Time.deltaTime * 10;
        // Time.deltaTime�� Ȱ���Ͽ� ������ �ǰ� ������ ���

        if(GameManager.instance.health < 0)// ������� 0���� ���� ���� �������� �ۼ�
        {
            for(int index = 2; index < transform.childCount; index++)
            // childCount : �ڽ� ������Ʈ�� ����
            // �̰��� ���� ������ �÷��̾�� �������� �ڽ� ������Ʈ�� ������ �ֱ� ����
            {
                transform.GetChild(index).gameObject.SetActive(false);
                // GetChild : �־��� �ε����� �ڽ� ������Ʈ�� ��ȯ�ϴ� �Լ�
                // transform.GetChild(index) -> �ڽĿ�����Ʈ�� Ʈ�������� ����
            }

            anim.SetTrigger("Dead");
            // �ִϸ����� SetTrigger �Լ��� ���� �ִϸ��̼� ����
            GameManager.instance.GameOver();


        }


    }

    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
        // Get<> : �����ʿ��� ������ ��Ʈ�� Ÿ��T���� �������� �Լ�
    }

}
