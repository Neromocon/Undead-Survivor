using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptble Object/ItemData")]
// CreateAssetMenu : Ŀ���� �޴��� �����ϴ� �Ӽ�
public class ItemData : ScriptableObject // * MonoBehaviour�� �ƴ�!!
{
    // �������� ���� �Ӽ����� ������ �ۼ�


    public enum ItemType { Melle, Range, Glove, Shoe, Heal }

    [Header("# Main Info")]
    public ItemType itemType;
    public int itemId;
    public string itemName;
    [TextArea]
    // �ν����Ϳ� �ؽ�Ʈ�� ���� �� ���� �� �ְ� TextArea �Ӽ� �ο�
    public string itemDesc;
    public Sprite itemIcon;

    [Header("# Level Data")]
    public float baseDamage;
    public int baseCount;
    public float[] damages;
    public int[] counts;

    [Header("# Weapon")]
    public GameObject projectile;
    public Sprite hand; // ��ũ��Ʈ�� ������Ʈ �ڵ忡�� �� ��������Ʈ�� ���� �Ӽ�



}
