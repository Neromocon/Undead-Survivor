using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("# Game Control")]
    // Header : �ν������� �Ӽ����� ���ڰ� ���н����ִ� Ÿ��Ʋ
    public float gameTime;
    public float maxGameTime = 2 * 10f;
    // ���ӽð��� �ִ� ���ӽð��� ����� ���� ����
    [Header("# Player Info")]
    public int health;
    public int maxHealth = 100;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 };
    [Header("# Game Object")]
    public PoolManager pool;
    // �پ��� ������ ���� ������ �� �ֵ��� ���ӸŴ����� Ǯ �Ŵ��� �߰�
    public Player player;

    void Awake()
    {
        instance = this;
        // Awake �����ֱ⿡�� �ν��Ͻ� ������ �ڱ��ڽ� this�� �ʱ�ȭ
    }

    void Start()
    {
        health = maxHealth;
        // ������ �� ���� ü�°� �ִ� ü���� ������ ���� �߰�
    }


    void Update()
    {
        gameTime += Time.deltaTime;
        // Ÿ�̸� �������� deltaTime�� ��� ���ϱ�. deltaTime : 1�������� �Һ��� �ð�.
        if (gameTime > maxGameTime)// Ÿ�̸Ӱ� ���� �ð� ���� �����ϸ� ��ȯ�ϵ��� �ۼ�
        {
            gameTime = maxGameTime;
            
        }

    }

    public void GetExp() // ����ġ ���� �Լ�
    {
        exp++;

        if(exp == nextExp[level]) // �������� �ʿ� ����ġ�� �����ϸ� ���� ���ϵ��� ����
        {
            level++;
            exp = 0;

        }
    }

}
