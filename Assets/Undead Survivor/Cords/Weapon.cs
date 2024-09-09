using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // 무기ID, 프리펩ID, 데미지, 개수, 속도 변수 선언
    public int id;
    public int prefabID;
    public float damage;
    public int count;
    public float speed;



    float timer;
    Player player;

    void Awake()
    {
        player = GetComponentInParent<Player>();
        // GetComponentInParent 함수로 부모의 컴포넌트 가져오기
    }


    void Start()
    {
        Init();
    }


    void Update()
    {
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);// z축만 움직여야 함.
                // **업데이트에서 이동이라던가 회전같은 변화가 있으면 꼭 델타타임을 적용시켜야 함
                break;
            //case 1:

            //    break;
            default:
                timer += Time.deltaTime; // Update에서 deltaTime을 계속 더하기
                if(timer > speed)
                {
                    timer = 0f;
                    Fire();
                }
                break;
        }

        // 테스트용
        if(Input.GetButtonDown("Jump"))
        {
            LevelUp(20, 5);
        }
    }

    // 레벨업 함수 작성
    public void LevelUp(float damage, int count)
    {
        this.damage = damage;        
        this.count += count;

        if(id == 0) // 속성 변경과 동시에 근접무기의 경우 배치도 필요하니 함수 호출        
            Bacth();
        
    }

    public void Init()
    {
        // 무기 ID에 따라 로직을 분리할 switch 문 작성
        switch (id)
        {
            case 0:
                speed = 150;                 
                Bacth();

                break;
            default:
                speed = 0.3f;
                // speed값은 연사속도를 의미 : 적을 수록 많이 발사
                break;
        }
    }

    void Bacth()
    {
        // for문으로 count 만큼 풀링에서 가져오기
        for(int index = 0; index < count; index++)
        {
            Transform bullet;

            if(index < transform.childCount) // 자신의 자식 오브젝트 개수 확인은 childCount 속성
            {
                bullet = transform.GetChild(index);
                // index가 아직 childCount 범위 내라면 GetChild 함수로 가져오기
               
            }
            else
            {
                bullet = GameManager.instance.pool.Get(prefabID).transform;
                // 가져온 오브젝트의 Transform을 지역변수로 저장
                bullet.parent = transform; // parent 속성을 통해 부모 변경
                // 즉, 근접무기가 생성되면 자기 자신(Weapon0) 아래(자식)로 생성됨
            }
            // 기존 으브젝트를 먼저 활용하고 모자란 것은 풀링에서 가져오기


            // 배치하면서 먼저 위치, 회전 초기화 하기
            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            Vector3 rotVec = Vector3.forward * 360 * index / count;
            bullet.Rotate(rotVec); // Rotate함수로 계산된 각도 적용
            bullet.Translate(bullet.up * 1.5f, Space.World); // Translate함수로 자신의 위쪽으로 이동
            // 이동 방향은 Space.World 기준으로

            bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero); // 근접무기이기 때문에 -1로 설정
            // -1은 무한히 관통함. 갈갈이? 슬랩찹!
        }
    }

    void Fire() //발사 로직
    {
        if(!player.scanner.nearestTarget) // 플레이어 가까이 적이 있는지를 조건으로 만들어야 이후 진행이 가능할듯.
            return;

        Vector3 targetPos = player.scanner.nearestTarget.position; // 위치
        Vector3 dir = targetPos - transform.position;  // 방향
        // 크기가 포함된 방향 : 목표 위치 = 나의 위치
        dir = dir.normalized;
        // normalized : 현재 벡터의 방향은 유지하고 크기를 1로 변환된 속성


        // 저장된 목표가 없으면 넘어가는 조건 로직 작성
        Transform bullet = GameManager.instance.pool.Get(prefabID).transform;
        bullet.position = transform.position;
        // 기존 생성 로직을 그대로 활용하면서 위치는 플레이어 위치로 지정
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        // FromToRotation : 지정된 축을 중심으로 목표를 향해 회전하는 함수
        bullet.GetComponent<Bullet>().Init(damage, count, dir);
        // 원거리 공격에 맞게 초기화 함수 호출하기
    }



}
