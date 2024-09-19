using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // 데미지와 관통 변수 선언
    public float damage;
    public int per;

    Rigidbody2D rigid; // 총알 스크립트에서 리지드바디2D 변수 선언 및 초기화

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // 변수 초기화 함수 생성
    public void Init(float damage, int per, Vector3 dir)
    {
        this.damage = damage;
        // this : 해당 클래스의 변수로 접근
        this.per = per;

        if (per >= 0) // 원거리 무기에만 적용할 것임
        {
            rigid.velocity = dir * 15f;            
        }
        //if(per > -1) // 관통이 -1(무한)보다 큰 것에 대해서는 속도 적용
        //{
        //    rigid.velocity = dir * 15f; // velocity : 속도
        //    // 속력을 곱해주어 총알이 날아가는 속도 증가시키기
        //}
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") || per == -100) //관통 로직 이전에 if문으로 조건 추가
            //per == -1에서 per == -100으로 변경
            return;

        per--;

        //if (per == -1) // 관통 값이 하나씩 줄어들면서 -1이 되면 비활성화/ 아래의 조건문으로 변경
        if (per < 0)//관통 이후의 로직을 감싸는 if 조건을 안전하게 변경
        {
            rigid.velocity = Vector2.zero; // 비활성화 이전에 미리 물리 속도 초기화
            gameObject.SetActive(false);
        }

    }

    // 투사체의 무한 이동에 대한 수정. OnTriggerExit2D이벤트와 Area를 활용하여 쉽게 비활성화
    void OnTriggerExit2D(Collider2D collision)
    {
        if(!collision.CompareTag("Area") || per == -100)
            return;//근접무기의 경우

        gameObject.SetActive(false);//플레이어의 에리어 밖으로 이동하면 비활성화 시킴
    }



}
