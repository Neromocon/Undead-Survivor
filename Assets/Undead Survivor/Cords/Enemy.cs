using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // 속도, 목표, 생존여부를 위한 변수 선언
    public float speed;
    public float health;
    public float maxHealth;
    public RuntimeAnimatorController[] animCon;
    // 에니메이터의 데이터는 AnimatorController가 담당함. 하지만 코드에서는 RuntimeAnimatorController가 담당함.
    public Rigidbody2D target;

    bool isLive;
    

    //리지드바디2D와 스프라이트렌더러를 위한 변수 선언
    Rigidbody2D rigid;
    Animator anim;
    // 애니메이터 변수 선언 및 초기화하고 이후 로직 작성하기
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
        // 몬스터가 살아있는 동안에만 움직이도록 조건 추가
        if(!isLive)
            return;

        Vector2 dirVec = target.position - rigid.position;
        // 태스트 중 UnassignedReferenceException : The variable target of Enemy has not been assigned.
        // 에러발생
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        // 방향 = 위치 차이의 정규화(Normalized)
        // 위치 차이 = 타겟 위치 - 나의 위치
        // 프레임의 영향으로 결과가 달라지지 않도록 FixedDeltaTime 사용.(대각선으로 움직일 시 상하좌우 이동 속도와 다름)
        rigid.MovePosition(rigid.position + nextVec);
        // rigid.position 지금의 위치 + nextVec 다음에 가야할 위치.
        // 플레이어의 키 입력 값을 더한 이동 = 몬스터의 방향 값을 더한 이동
        rigid.velocity = Vector2.zero;
        // 충돌 시 반대방향으로 밀려남. 이것은 벨로시티가 존재하기 때문. 그래서 벨로시티를 0으로 설정해
        // 물리 속도가 이동에 영향을 주지 않도록 속도 제거

    }

    void LateUpdate()//플레이어 위치에 따라 방향 전환
    {
        if (!isLive)
            return;

        spriter.flipX = target.position.x < rigid.position.x;
        // 목표의 X축 값과 자신의 X축 값을 비교하여 작으면 true가 되도록 설정
    }

    // OnEnable : 스크립트가 활성화 될 때, 호출되는 이벤트 함수
    void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        // 스크립트가 활성화 될 때 타겟을 초기화 하는 함수. 
        isLive = true;
        // OnEnable에서 생존여부와 체력 초기화
        health = maxHealth;
        // 폴링에 의해 되살아 났을 때 0이거나 0보다 작은 수가 나오면 안되기에 maxHealth를 만들고
        // 적용함.
    }

    // 초기 속성을 적용하는 함수 추가
    public void Init(SpawnData data)  // 매개변수로 소환데이터 하나 지정 
    {
        anim.runtimeAnimatorController = animCon[data.spriteType];
        // 매개변수의 속성을 몬스터 속성 변경에 활용하기
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // OnTriggerEnter2D 매개변수의 태그를 조건으로 활용
        if (!collision.CompareTag("Bullet"))
            return;

        health -= collision.GetComponent<Bullet>().damage;
        // Bullet 컴포넌트로 접근하여 데미지를 가져와 피격 계산하기.
        // health자체에서 collision.GetComponent<Bullet>().damage에 해당하는 숫자를 알아서 빼줌

        if(health > 0)// 남은 체력을 조건으로 피격과 사망으로 로직을 나누기
        {
            // 아직 살아있음. 피격 액션

        }
        else
        {
            // 끄앙 죽음
            Dead();
        }


    }

    void Dead()
    {
        // 사망할 땐 SetActive 함수를 통한 오브젝트 비활성화. 테스트용
        gameObject.SetActive(false);
    }


}
