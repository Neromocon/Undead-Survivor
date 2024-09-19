using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    Collider2D coll; // ��� �ݶ��̴��� �⺻ ������ �� �ƿ츣�� Ŭ����

    void Awake()
    {
        coll = GetComponent<Collider2D>();
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        // OnTriggerExit2D�� �Ű����� ���� �ݶ��̴��� �±׸� ��������.
        if (!collision.CompareTag("Area"))
            return;
        Vector3 playerPos = GameManager.instance.player.transform.position;
        // �Ÿ��� ���ϱ� ���� �÷��̾� ��ġ�� Ÿ�ϸ� ��ġ�� �̸� ����
        Vector3 myPos = transform.position;
        // ���� ��ġ
        //float diffX = Mathf.Abs(playerPos.x - myPos.x); //�÷��̾� ��ġ - Ÿ�ϸ� ��ġ ������� �Ÿ� ���ϱ�
        //// ������ ����� �Ǿ�� �� ��, ���밪�� �ʿ�. ���� Mathf.Abs()�Լ��� �����. �̰��� ������ ����� ������ִ� ���밪 �Լ���
        //float diffY = Mathf.Abs(playerPos.y - myPos.y);
        // => ����ġ ���� �׶��� ���̽��� �̵�
        //Vector3 playerDir = GameManager.instance.player.inputVec;
        // �÷��̾��� �̵� ������ �����ϱ� ���� ���� �߰�
        //float dirX = playerDir.x < 0 ? -1 : 1;
        //float dirY = playerDir.y < 0 ? -1 : 1;
        // =>> ĳ������ �����Է½� ����� �Ǵµ� �ش� ������ �ް��� �̵����� ���� ������ �߻�        

        switch (transform.tag)//���� ����
        {
            case "Ground":
                float diffX = playerPos.x - myPos.x;                                                                 
                float diffY = playerPos.y - myPos.y;
                float dirX = diffX < 0 ? -1 : 1;
                float dirY = diffY < 0 ? -1 : 1;
                diffX = Mathf.Abs(diffX);
                diffY = Mathf.Abs(diffY);
                // �÷��̾�� Ÿ�ϸ��� �Ÿ� ���̸� ����� �ؼ� ���̳ʽ��̸� Ÿ���� �÷��̾� ���� ���ʿ� �ְ�, �÷����� Ÿ�ϸ��� �÷��̾��� �����ʿ� �ִ�. �̷������� ����
                // �� ������Ʈ�� ��ġ ���̸� Ȱ���� �������� ����

                if (diffX > diffY)// �� ������Ʈ�� �Ÿ����̿��� X���� Y�ຸ�� ũ�� �����̵�
                {
                    transform.Translate(Vector3.right * dirX * 40);
                    // Translate : ������ �� ��ŭ ���� ��ġ���� �̵�
                    // Translate(Vector3.right * dirX * 40) �������� 40�� ���� ����
                    // => �� ũ�Ⱑ 20�ε� ���� �̵� �� �� ĭ�� �̵��ؾ� �ϱ⿡ 2�� ���� 40�� ����.
                }
                else if (diffX < diffY)
                {
                    transform.Translate(Vector3.up * dirY * 40);                    
                }
                break;
            case "Enemy": // �̰����� ���ʹ� ���ġ ���� ���
                if(coll.enabled) // �ݶ��̴��� Ȱ��ȭ �Ǿ��ִ��� ���� ���� �ۼ�
                {
                    Vector3 dist = playerPos - myPos; //�÷��̾� ��ġ - ���� ��ġ
                    Vector3 ran = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), 0);
                    // ���� ���͸� ���Ͽ� �����ִ� ���� ���ġ �����
                    transform.Translate(ran + dist * 2);// <= ����. �� ������Ʈ�� �Ÿ��� �״�� Ȱ���ϴ� ���� ����Ʈ
                    //transform.Translate(playerDir * 20 + new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0f));
                    // �÷��̾��� �̵� ���⿡ ���� ���� ���� �����ϵ��� �̵�
                    // ������ ��ġ���� �����ϵ��� ���� ���ϱ�
                }
                break;

        }
    }
}
