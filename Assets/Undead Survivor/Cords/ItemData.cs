using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptble Object/ItemData")]
// CreateAssetMenu : 커스텀 메뉴를 생성하는 속성
public class ItemData : ScriptableObject // * MonoBehaviour가 아님!!
{
    // 아이템의 각종 속성들을 변수로 작성


    public enum ItemType { Melle, Range, Glove, Shoe, Heal }

    [Header("# Main Info")]
    public ItemType itemType;
    public int itemId;
    public string itemName;
    [TextArea]
    // 인스펙터에 텍스트를 여러 줄 넣을 수 있게 TextArea 속성 부여
    public string itemDesc;
    public Sprite itemIcon;

    [Header("# Level Data")]
    public float baseDamage;
    public int baseCount;
    public float[] damages;
    public int[] counts;

    [Header("# Weapon")]
    public GameObject projectile;
    public Sprite hand; // 스크립트블 오브젝트 코드에서 손 스프라이트를 담을 속성



}
