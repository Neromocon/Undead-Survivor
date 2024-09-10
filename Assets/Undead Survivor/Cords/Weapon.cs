using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // ����ID, ������ID, ������, ����, �ӵ� ���� ����
    public int id;
    public int prefabID;
    public float damage;
    public int count;
    public float speed;



    float timer;
    Player player;

    void Awake()
    {
        player = GameManager.instance.player;
        // GetComponentInParent �Լ��� �θ��� ������Ʈ ��������
        // -> Awake �Լ������� �÷��̾� �ʱ�ȭ�� ���ӸŴ��� Ȱ������ ����
    }

    void Update()
    {
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);// z�ุ �������� ��.
                // **������Ʈ���� �̵��̶���� ȸ������ ��ȭ�� ������ �� ��ŸŸ���� ������Ѿ� ��
                break;
            //case 1:

            //    break;
            default:
                timer += Time.deltaTime; // Update���� deltaTime�� ��� ���ϱ�
                if(timer > speed)
                {
                    timer = 0f;
                    Fire();
                }
                break;
        }

        // �׽�Ʈ��
        if(Input.GetButtonDown("Jump"))
        {
            LevelUp(20, 5);
        }
    }

    // ������ �Լ� �ۼ�
    public void LevelUp(float damage, int count)
    {
        this.damage = damage;        
        this.count += count;

        if(id == 0) // �Ӽ� ����� ���ÿ� ���������� ��� ��ġ�� �ʿ��ϴ� �Լ� ȣ��        
            Bacth();
        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
        // BroadcastMessage�� �ʱ�ȭ, ������ �Լ� ������ �κп��� ȣ��
    }

    public void Init(ItemData data)
    // Weapon �ʱ�ȭ �Լ��� ��ũ��Ʈ�� ������Ʈ�� �Ű������� �޾� Ȱ��
    {
        // Basic Set
        name = "Weapon" + data.itemId;
        transform.parent = player.transform;
        // ���⸦ �÷��̾��� �ڽ����� �����ؾ� ��
        transform.localPosition = Vector3.zero;
        // ���� ��ġ�� localPosition�� �������� ����

        // Property Set
        id = data.itemId;
        damage = data.baseDamage;
        count = data.baseCount;
        // ���� ���� �Ӽ� �������� ��ũ��Ʈ�� ������Ʈ �����ͷ� �ʱ�ȭ

        for (int index = 0; index < GameManager.instance.pool.prefabs.Length; index++)
        {
            if(data.projectile == GameManager.instance.pool.prefabs[index])
            // ������ ���̵�� Ǯ�� �Ŵ����� �������� ã�Ƽ� �ʱ�ȭ
            {
                prefabID = index;
                break;
            }
        }
        // ���̷��� �ϴ���, ��ũ��Ʈ�� ������Ʈ�� �������� ���ؼ� �ε����� �ƴ� ���������� ����.
        // ��, ���������� �����ϸ� ���� �� �Ѳ����� ���� ����? ��������


        // ���� ID�� ���� ������ �и��� switch �� �ۼ�
        switch (id)
        {
            case 0:
                speed = 150;                 
                Bacth();

                break;
            default:
                speed = 0.3f;
                // speed���� ����ӵ��� �ǹ� : ���� ���� ���� �߻�
                break;
        }

        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
        // BroadcastMessage : Ư�� �Լ� ȣ���� ��� �ڽĿ��� ����ϴ� �Լ�
        // BroadcastMessage�� �ι�° ���ڰ����� SendMessageOptions�� DontRequireReceiver�� �߰�


    }

    void Bacth()
    {
        // for������ count ��ŭ Ǯ������ ��������
        for(int index = 0; index < count; index++)
        {
            Transform bullet;

            if(index < transform.childCount) // �ڽ��� �ڽ� ������Ʈ ���� Ȯ���� childCount �Ӽ�
            {
                bullet = transform.GetChild(index);
                // index�� ���� childCount ���� ����� GetChild �Լ��� ��������
               
            }
            else
            {
                bullet = GameManager.instance.pool.Get(prefabID).transform;
                // ������ ������Ʈ�� Transform�� ���������� ����
                bullet.parent = transform; // parent �Ӽ��� ���� �θ� ����
                // ��, �������Ⱑ �����Ǹ� �ڱ� �ڽ�(Weapon0) �Ʒ�(�ڽ�)�� ������
            }
            // ���� ������Ʈ�� ���� Ȱ���ϰ� ���ڶ� ���� Ǯ������ ��������


            // ��ġ�ϸ鼭 ���� ��ġ, ȸ�� �ʱ�ȭ �ϱ�
            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            Vector3 rotVec = Vector3.forward * 360 * index / count;
            bullet.Rotate(rotVec); // Rotate�Լ��� ���� ���� ����
            bullet.Translate(bullet.up * 1.5f, Space.World); // Translate�Լ��� �ڽ��� �������� �̵�
            // �̵� ������ Space.World ��������

            bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero); // ���������̱� ������ -1�� ����
            // -1�� ������ ������. ������? ������!
        }
    }

    void Fire() //�߻� ����
    {
        if(!player.scanner.nearestTarget) // �÷��̾� ������ ���� �ִ����� �������� ������ ���� ������ �����ҵ�.
            return;

        Vector3 targetPos = player.scanner.nearestTarget.position; // ��ġ
        Vector3 dir = targetPos - transform.position;  // ����
        // ũ�Ⱑ ���Ե� ���� : ��ǥ ��ġ = ���� ��ġ
        dir = dir.normalized;
        // normalized : ���� ������ ������ �����ϰ� ũ�⸦ 1�� ��ȯ�� �Ӽ�


        // ����� ��ǥ�� ������ �Ѿ�� ���� ���� �ۼ�
        Transform bullet = GameManager.instance.pool.Get(prefabID).transform;
        bullet.position = transform.position;
        // ���� ���� ������ �״�� Ȱ���ϸ鼭 ��ġ�� �÷��̾� ��ġ�� ����
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        // FromToRotation : ������ ���� �߽����� ��ǥ�� ���� ȸ���ϴ� �Լ�
        bullet.GetComponent<Bullet>().Init(damage, count, dir);
        // ���Ÿ� ���ݿ� �°� �ʱ�ȭ �Լ� ȣ���ϱ�
    }



}
