using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gear : MonoBehaviour
{
    public ItemData.ItemType type;
    public float rate;
    // 장비의 타입과 수치를 저장할 변수 선언

    public void Init(ItemData data) // Weapon과 동일하게 초기화 함수 작성
    {
        // Basic Set
        name = "Gear" + data.itemId;
        transform.parent = GameManager.instance.player.transform;
        transform.localScale = Vector3.zero;

        // Property Set
        type = data.itemType;
        rate = data.damages[0];
        ApplyGear();
        // ApplyGear()를 호출하는게 관건인데 언제 하느냐? 이 기어가 처음 생성 됬을 때다.
        // 생성되자 마자 이 플레이어가 가지고 있는 모든 무기들에게 이 기어에 대한 기능을 다 적용시켜야 함.

    }

    public void LevelUp(float rate)
    {
        this.rate = rate;
        ApplyGear();
        // 장비가 새롭게 추가되거나 레벨업 할 때 로직적용 함수를 호출
    }


    // 타입에 따라 적절하게 로직을 적용시켜주는 함수 추가
    void ApplyGear()
    {
        switch (type)
        {
            case ItemData.ItemType.Glove:
                RateUp();
                break;
            case ItemData.ItemType.Shoe:
                SpeedUp();
                break;
        }
    }



    void RateUp() // 장갑의 기능인 연사력을 올리는 함수
    {
        Weapon[] weapons = transform.parent.GetComponentsInChildren<Weapon>();
        // 플레이어로 올라가서 모든 Weapon을 가져오기

        // foreach문으로 하나씩 순회하면서 타입에 따라 속도 올리기
        foreach(Weapon weapon in weapons)
        {
            switch(weapon.id)
            {
                case 0:
                    weapon.speed = 150 + (150 * rate);
                    break;
                default :
                    weapon.speed = 0.5f * (1f - rate);
                    break;
            }
        }
    }

    // 신발의 기능인 이동 속도를 올리는 함수 작성
    void SpeedUp()
    {
        float speed = 3;
        GameManager.instance.player.speed = speed + speed * rate;
    }


}
