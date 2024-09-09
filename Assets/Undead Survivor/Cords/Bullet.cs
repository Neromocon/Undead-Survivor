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

        if(per > -1) // 관통이 -1(무한)보다 큰 것에 대해서는 속도 적용
        {
            rigid.velocity = dir * 15f; // velocity : 속도
            // 속력을 곱해주어 총알이 날아가는 속도 증가시키기
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") || per == -1) //관통 로직 이전에 if문으로 조건 추가
            return;

        per--;

        if(per == -1) // 관통 값이 하나씩 줄어들면서 -1이 되면 비활성화
        {
            rigid.velocity = Vector2.zero; // 비활성화 이전에 미리 물리 속도 초기화
            gameObject.SetActive(false);
        }

    }


}
