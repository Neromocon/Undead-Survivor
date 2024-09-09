using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float gameTime;
    public float maxGameTime = 2 * 10f;
    // ���ӽð��� �ִ� ���ӽð��� ����� ���� ����

    public PoolManager pool;
    // �پ��� ������ ���� ������ �� �ֵ��� ���ӸŴ����� Ǯ �Ŵ��� �߰�
    public Player player;

    void Awake()
    {
        instance = this;
        // Awake �����ֱ⿡�� �ν��Ͻ� ������ �ڱ��ڽ� this�� �ʱ�ȭ
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

}
