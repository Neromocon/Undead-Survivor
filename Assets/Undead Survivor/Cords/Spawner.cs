using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    // �ڽ� ������Ʈ�� Ʈ�������� ���� �迭 ���� ����
    public SpawnData[] spawnData;



    int level; // ��ȯ ��ũ��Ʈ���� ���� ��� ���� ����
    float timer; //��ȯ Ÿ�̸Ӹ� ���� ���� ����

    void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
        // GetComponentsInChildren �Լ��� �ʱ�ȭ. * GetComponentInChildren �� ������ �Լ���.
        // GetComponent != GetComponents ������ s�� ����!

    }


    void Update()
    {
        timer += Time.deltaTime;
        // Ÿ�̸� �������� deltaTime�� ��� ���ϱ�. deltaTime : 1�������� �Һ��� �ð�.
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 10f), spawnData.Length - 1);
        // ������ ���ڷ� ������ �ð��� ���� ������ �ö󰡵��� �ۼ�
        // �״�� �����ϸ� �Ҽ������� ���� �����µ� level�� ���� Ÿ���̶� ������ ������.
        // ���� Mathf�Լ��� FloorToInt�� �����.
        // FloorToInt : �Ҽ��� �Ʒ��� ������ Int������ �ٲٴ� �Լ�
        // CeilToInt : �Ҽ��� �Ʒ��� �ø��� Int������ �ٲٴ� �Լ� *����
        // IndexOutOfRangeException : Index was outside the bounds of the array. ��� ���� �߻�.
        // level�� ũ�⸦ �Ѿ ���� �Ǿ �߻��� ������ ����. Mathf.Min�߰�
        // �ε��� ������ ���� ���� ��� �� Min�Լ��� ����Ͽ� ���� �� ����
        // Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 10f), spawnData.Length - 1)
        // �̷��� �ϸ� �ε������� ���� �Ѿ�� �ʱ� ������ ���� �߻��� ���� �� ����.
        if (timer > spawnData[level].spawnTime)// Ÿ�̸Ӱ� ���� �ð� ���� �����ϸ� ��ȯ�ϵ��� �ۼ�
        {
            timer = 0f;
            Spawn();
        }
        
    }

    void Spawn()// ��ȯ �Լ��� ���� �ۼ�
    {
        GameObject enemy = GameManager.instance.pool.Get(0);
        // Ǯ �Լ����� ���� ���� ���� �ֵ��� ����
        // instance ��ȯ ���� ������ �־�α�
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        // ������  ��ȯ ��ġ �� �ϳ��� ��ġ�ǵ��� �ۼ�
        // �� ���� ���� ���� ������ 0�� �ƴ϶� 1�ΰ�, �װ��� �ڱ� �ڽŵ� �����ϱ� ����
        // ��, �ڽ� ������Ʈ������ ���õǵ��� ���� ������ 1���� �ؾ���.
        enemy.GetComponent<Enemy>().Init(spawnData[level]);
        // ������Ʈ Ǯ���� ������ ������Ʈ���� Enemy ������Ʈ�� ����
        // ���Ӱ� �ۼ��� �Լ��� ȣ���ϰ� ��ȯ������ ���ڰ� ����
    }

}

// ** ����ȭ(Serialization) : ��ü�� ���� Ȥ�� �����ϱ� ���� ��ȯ
[System.Serializable] // �̷��� �ϸ� �ٷ� �Ʒ��� ��ü�� ����ȭ�� ��.
// ����ȭ�� �Ϸ��� ����ȭ�� �� ��ü �ٷ� ���� [System.Serializable]�� �߰��ؾ� ��.
// []�� �迭�� ���ϱ⵵ ������ �Ӽ��� ���ϱ⵵ ��.
// ���� �ۼ��� Ŭ������ ����ȭ�� ���Ͽ� �ν����Ϳ��� �ʱ�ȭ ����
public class SpawnData // ��ȯ �����͸� ����ϴ� Ŭ���� ����
{
    // �߰��� �Ӽ��� : ��������Ʈ Ÿ��, ��ȯ�ð�, ü��, �ӵ�
    public float spawnTime;
    public int spriteType;
    public int health;
    public float speed;


}


