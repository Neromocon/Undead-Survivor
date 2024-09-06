using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // 속도, 목표, 생존여부를 위한 변수 선언
    public float speed;
    public Rigidbody2D target;

    bool isLive = true;
    

    //리지드바디2D와 스프라이트렌더러를 위한 변수 선언
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
        // 몬스터가 살아있는 동안에만 움직이도록 조건 추가
        if(!isLive)
            return;

        Vector2 dirVec = target.position - rigid.position;
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


}
