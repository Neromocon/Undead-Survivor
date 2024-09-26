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

    public GameObject Gold;
    [SerializeField]
    private int gold = 10; // 적 사망시 획득 가능한 골드

    //리지드바디2D와 스프라이트렌더러를 위한 변수 선언
    Rigidbody2D rigid;
    Collider2D coll;
    Animator anim;
    // 애니메이터 변수 선언 및 초기화하고 이후 로직 작성하기
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

        // 몬스터가 살아있는 동안에만 움직이도록 조건 추가
        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            // GetCurrentAnimatorStateInfo : 현재 상태 정보를 가져오는 함수
            // IsName : 해당 상태의 이름이 지정된 것과 같은지 확인하는 함수
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
        if (!GameManager.instance.isLive)
            return;

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
        coll.enabled = true;        
        rigid.simulated = true;        
        spriter.sortingOrder = 2;
        anim.SetBool("Dead", false);
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
        if (!collision.CompareTag("Bullet") || !isLive) 
            //사망 로직이 연달아 실행되는 것을 방지하기 위해 조건 추가
            return;

        health -= collision.GetComponent<Bullet>().damage;
        // Bullet 컴포넌트로 접근하여 데미지를 가져와 피격 계산하기.
        // health자체에서 collision.GetComponent<Bullet>().damage에 해당하는 숫자를 알아서 빼줌

        StartCoroutine(KnockBack());
        // GetCurrentAnumatorStateInfo : 현재 상태 정보를 가져오는 함수

        if(health > 0)// 남은 체력을 조건으로 피격과 사망으로 로직을 나누기
        {
            // 아직 살아있음. 피격 액션
            anim.SetTrigger("Hit");
            //피격 부분에 애니메이터의 SetTrigger 함수를 호출하여 상태 변경
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Hit);
        }
        else// 끄앙 죽음
        {
            isLive = false;
            coll.enabled = false;
            // 컴포넌트의 비활성화는 .enabled = false
            rigid.simulated = false;
            // 리지드바디의 물리적 비활성화는 .simulated = false
            spriter.sortingOrder = 1;
            anim.SetBool("Dead", true);
            GameManager.instance.kill++;
            GameManager.instance.GetExp();

            if (GameManager.instance.isLive)//언데드 사망 사운드는 게임 종료 시에는 나지 않도록 조건 추가. 게임 승리시 모든 몬스터가 죽는데 이때 사망 사운드가 한꺼번에 나타나므로 플레이어에게 큰 영향을 끼치기 때문.
            {
                AudioManager.instance.PlaySfx(AudioManager.Sfx.Dead);
            }

            GameObject gold = Instantiate(Gold, transform.position, Quaternion.identity);
            //몬스터가 죽은 위치에 골드 생성
            GameManager.instance.PlayerGold += 10;
            // 게임메니저의 PlayerGold에 골드량을 증가시켜줌

        }


    }

    IEnumerator KnockBack()
    {
        yield return wait; // 리턴 값을 null로 하면 1프레임 쉬기
        // 하나의 물리 프레임을 딜레이 줘야함.
        //yield return new WaitForSeconds(2f);  // 2초 쉬기
        // yield return을 통해 다양한 쉬는 시간을 지정
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        // 플레이어 기준의 반대 방향 : 현재 위치 - 플레이어 위치
        rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
        // 리지드바디2D의 AddForce 함수로 힘 가하기
        // 순간적인 힘이므러 ForceMode2D.Impulse 속성 추가
    }
    // 코루틴(Coroutine) : 생명 주기와 비동기처럼 실행되는 함수
    // IEnumerator : 코루틴만의 반환형 인터페이스
    // yield : 코루틴의 반환 키워드

    void Dead()
    {
        // 사망할 땐 SetActive 함수를 통한 오브젝트 비활성화. 테스트용
        gameObject.SetActive(false);
    }


}
