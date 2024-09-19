using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;
    public Scanner scanner; // 플레이어 스크립트에서 검색 클래스 타입 변수 선언 및 초기화
    public Hand[] hands;
    public RuntimeAnimatorController[] animCon;
    // 플레이어 스크립트에 여러 애니메이터 컨트롤러를 저장할 배열 변수 선언

    Rigidbody2D rigid;
    // 게임 오브젝트의 리지드바디 2D를 저장할 변수 선언
    SpriteRenderer spriter;
    Animator anim;

    // Start is called before the first frame update
    void Awake()//시작할 때 한번만 실행되는 생명주기 
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        // GetComponent<T> : 오브젝트에서 컴포넌트를 가져오는 함수. T자리에 컴포넌트 이름 작성
        anim = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
        hands = GetComponentsInChildren<Hand>(true);
        // 플레이어에서 손 스크립트를 담을 배열변수 선언 및 초기화
        // 함수 인자 값을 추가 하지 않을 시 플레이어 오브젝트의 무기들은 비활성화 되었기 때문에 대상에서 제외가 됨
        // 따라서 인자값을 true로 설정하면 활성화 가능
    }

    void OnEnable()
    {
        speed *= Character.Speed;
        anim.runtimeAnimatorController = animCon[GameManager.instance.playerId];
    }

    // Update is called once per frame
    // => Update : 하나의 프레임마다 한번씩 호출되는 생명주기 함수.
    //   -> Ex) 60프레임이면 1초에 60번 실행이 되는 형식.
    void Update()
    {
        if (!GameManager.instance.isLive)
            return;

        //inputVec.x = Input.GetAxisRaw("Horizontal");
        // inputVec.x = Input.GetAxis("Horizontal");
        //inputVec.y = Input.GetAxisRaw("Vertical");
        // Input : 유니티에서 받는 모든 입력을 관리하는 클래스.
        // GetAxis() : 축에 대한 값. 어떤 축이라고 물어보면 문자열로 넣음.
        // 유니티 에디터에서 Edit -> Project -> Input Manager에서 버튼이름 확인 가능.
        // Input Manager는 물리적인 입력을 지정된 버튼으로 연결하는 역할.
        // Axis 값은 -1부터 1까지.

        // 에디터에서 이동시 관성 비스무리한게 있는데 이것은 Input.GetAxis 때문
        // Input.GetAxis는 입력 값이 부드럽게 바꿔줌.
        // GetAxisRaw로 더욱 명확한 컨트롤 구현 가능

        //===============
        // Input Manager가 아닌 Input System으로 변환함.



    }

    void FixedUpdate()
    //FixedUpdate : 물리 연산 프레임마다 호출되는 생명주기 함수.
    {
        if (!GameManager.instance.isLive)
            return;

        Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;
        // normalized : 벡터 값의 크기가 1이 되도록 좌표가 수정된 값.
        // 피타고라스 정의를 생각해 보자. 위로 1픽셀로 움직이거나 오른쪽으로 1픽셀 움직이는 길이랑
        // 다르게 오른쪽 위로 움직이는 길이는 다르다.
        // fixedDeltaTime : 물리 프레임 하나가 소비한 시간.
        // Input Systemㄴ를 사용하기에 normalized를 뺌. 이미 설정에서 normalized를 적용시킴.

        // 물리이동 첫번째 : 힘을 준다 == AddForce
        //rigid.AddForce(inputVec);

        // 물리이동 두번째 : 속도를 직접 제어한다 == Velocity
        //rigid.velocity = inputVec;

        // 물리이동 세번째 : 위치를 옮긴다 == MovePosition / 체스말을 움직이듯이, 순간이동 하듯이.
        rigid.MovePosition(rigid.position + nextVec);
        // MovePosition는 위치 이동이라 현재 위치도 더해주어야 함.
        // 따라서 rigid.position을 더해줌.
        // 이대로 하면 문제가 생김. 프레임에 따라 속도가 변함
        // 따라서 다른 프레임 환경에서도 이동거리는 같아야 함.



    }

    

    void LateUpdate()
    // LateUpdate : 프레임이 종료 되기 전 실행되는 생명주기 함수
    {
        if (!GameManager.instance.isLive)
            return;

        anim.SetFloat("Speed", inputVec.magnitude);
        // SetFloat 첫번째 인자 : 파라메타 이름
        // SetFloat 두번째 인자 : 변경할 float 값
        // magnitude : 벡터의 순수한 크기 값


        if (inputVec.x != 0)
        {
            spriter.flipX = inputVec.x < 0;
        }
    }

    void OnCollisionStay2D(Collision2D collision)//피격 로직
    {
        if(!GameManager.instance.isLive)
            return;

        GameManager.instance.health -= Time.deltaTime * 10;
        // Time.deltaTime을 활용하여 적절한 피격 데미지 계산

        if(GameManager.instance.health < 0)// 생명력이 0보다 작을 때를 조건으로 작성
        {
            for(int index = 2; index < transform.childCount; index++)
            // childCount : 자식 오브젝트의 개수
            // 이것을 쓰는 이유가 플레이어는 여러개의 자식 오브젝트를 가지고 있기 때문
            {
                transform.GetChild(index).gameObject.SetActive(false);
                // GetChild : 주어진 인덱스의 자식 오브젝트를 반환하는 함수
                // transform.GetChild(index) -> 자식오브젝트의 트랜스폼이 나옴
            }

            anim.SetTrigger("Dead");
            // 애니메이터 SetTrigger 함수로 죽음 애니메이션 실행
            GameManager.instance.GameOver();


        }


    }

    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
        // Get<> : 프로필에서 설정한 컨트롤 타입T값을 가져오는 함수
    }

}
