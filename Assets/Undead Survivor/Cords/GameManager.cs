using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Player player;

    void Awake()
    {
        instance = this;
        // Awake �����ֱ⿡�� �ν��Ͻ� ������ �ڱ��ڽ� this�� �ʱ�ȭ
    }

}
