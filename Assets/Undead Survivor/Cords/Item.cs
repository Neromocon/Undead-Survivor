using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemData data;
    public int level;
    public Weapon weapon;
    // 아이템 관리에 필요한 변수들 선언
    public Gear gear; // 버튼 스크립트에서 새롭게 작성한 장비 타입의 변수 선언


    Image icon;
    Text textLevel;

    void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1]; //여러개이므로 배열
        // 자식 오브젝트의 컴포넌트가 필요하므로 GetComponentsInChildren 사용. s임 중요함
        // GetComponentsInChildren에서 두번째 값으로 가져오기 (첫번째는 자기자신이므로)
        icon.sprite = data.itemIcon;

        Text[] texts = GetComponentsInChildren<Text>();
        textLevel = texts[0];

    }

    // LateUpdate에서 레벨 텍스트 경신
    void LateUpdate()
    {
        textLevel.text = "Lv." + (level + 1);
    }

    // 버튼 클릭 이벤트와 연결할 함수 추가
    public void OnClick()
    {
        // 아이템 타입을 통해 switch case 문 작성해두기
        switch (data.itemType)
        {
            case ItemData.ItemType.Melle :
            case ItemData.ItemType.Range :
                if(level == 0)
                {
                    GameObject newWeapon = new GameObject();
                    // 새로운 게임으브젝트를 코드로 생성
                    weapon = newWeapon.AddComponent<Weapon>();
                    // AddComponent<T> : 게임오브젝트에 T 컴포넌트를 추가하는 함수
                    // AddComponent 함수 반환 값을 미리 선언한 변수에 저장
                    weapon.Init(data);
                }
                else
                {
                    float nextDamage = data.baseDamage;
                    int nextCount = 0;

                    nextDamage += data.baseDamage * data.damages[level]; // 레벨에 따라서 증가 된 데미지를 가져옴. 증가된 되미지는 백분율로 설정하였기 때문에 베이스 데미지를 곱해줌.
                    // 처음 이후의 레벨업은 데미지와 횟수를 계산
                    nextCount += data.counts[level];

                    weapon.LevelUp(nextDamage, nextCount);
                    // Weapon 클래스의 작성된 레벨업 함수를 활용
                }
                level++;
                break;                            
            case ItemData.ItemType.Glove :
            case ItemData.ItemType.Shoe :
                if (level == 0)
                {
                    GameObject newGear = new GameObject();                    
                    gear = newGear.AddComponent<Gear>();
                    gear.Init(data);
                }
                else
                {
                    float nextRate = data.damages[level];
                    gear.LevelUp(nextRate);
                }
                level++;
                //레벨 값을 올리는 로직을 무기, 장비 case 안쪽으로 이동
                break;                
            case ItemData.ItemType.Heal :
                GameManager.instance.health = GameManager.instance.maxHealth;
                // 치료 기능의 음료수 로직은 case 문에서 바로 작성
                break;

        }        

        if(level == data.damages.Length)// 최대 레벨 도달을 조건으로 만듬
        // 스크립트블 오브젝트에 작성한 레벨 데이터 개수를 넘기지 않게 로직 추가
        {
            GetComponent<Button>().interactable = false;
        }
    }


}
