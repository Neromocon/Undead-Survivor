using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("# Game Control")]
    // Header : �ν������� �Ӽ����� ���ڰ� ���н����ִ� Ÿ��Ʋ
    public bool isLive;
    //public bool isActiveScene;
    // �ð� ���� ���θ� �˷��ִ� bool ���� ����
    public float gameTime;
    public float maxGameTime = 2 * 10f;
    // ���ӽð��� �ִ� ���ӽð��� ����� ���� ����
    [Header("# Player Info")]
    public int playerId;
    public float health;
    public float maxHealth = 100; // ����� ���� ������ float������ ����
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 };
    //public int Gold;
    public int PlayerGold;
    [Header("# Game Object")]
    public PoolManager pool;
    // �پ��� ������ ���� ������ �� �ֵ��� ���ӸŴ����� Ǯ �Ŵ��� �߰�
    public Player player;
    public LevelUp uiLevelUp;
    public Result uiResult; // ���Ӱ�� UI ������Ʈ�� ������ ���� ���� �� �ʱ�ȭ
    // ������ Ÿ���� Result���� ����
    public Transform uiJoy;
    public GameObject enemyCleaner;

    
    

    void Awake()
    {
        instance = this;
        // Awake �����ֱ⿡�� �ν��Ͻ� ������ �ڱ��ڽ� this�� �ʱ�ȭ
        Application.targetFrameRate = 60;
        // targetFrameRate : ���� ������ ���ڴ�� �����ӷ��� ���� �Ѵ�
    }

    public void GameStart(int id)// ���ӸŴ����� ���� Start�Լ��� GameStart�� ����
    {
        playerId = id;
        health = maxHealth;// ������ �� ���� ü�°� �ִ� ü���� ������ ���� �߰�

        player.gameObject.SetActive(true); // ���� ������ �� �÷��̾� Ȱ��ȭ �� �⺻ �α� ����
        uiLevelUp.Select(playerId % 2);// ���� ���� ������ ���� �Լ� ȣ�⿡�� ���� ���� ĳ����ID�� ����
        //uiLevelUp.Select(0);//�ӽ� ��ũ��Ʈ (ù��° ĳ���� ����)

        isLive = true;
        Resume();

        AudioManager.instance.PlayBgm(true);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
        //ȿ������ ����� �κи��� ����Լ� ȣ��
        //������ ������ �� ĳ���͸� �����ؾ� �ϹǷ� Sfx.Select�� ������
    }

    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine() // �����̸� ���� ���ӿ��� �ڷ�ƾ �ۼ�
    {
        isLive = false;

        yield return new WaitForSeconds(0.5f);

        uiResult.gameObject.SetActive(true);// ���Ӱ�� UI ������Ʈ�� ���ӿ��� �ڷ�ƾ���� Ȱ��ȭ
        uiResult.Lose();
        Stop();

        AudioManager.instance.PlayBgm(false);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Lose);
    }
    public void GameVictroy()
    {
        StartCoroutine(GameVictroyRoutine());
    }
    IEnumerator GameVictroyRoutine() // �����̸� ���� ���ӿ��� �ڷ�ƾ �ۼ�
    {
        isLive = false;
        enemyCleaner.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        uiResult.gameObject.SetActive(true);// ���Ӱ�� UI ������Ʈ�� ���ӿ��� �ڷ�ƾ���� Ȱ��ȭ
        uiResult.Win();
        Stop();

        AudioManager.instance.PlayBgm(false);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Win);
    }


    public void GameRetry()
    {
        SceneManager.LoadScene(0);
        // LoadScene : �̸� Ȥ�� �ε����� ����� ���Ӱ� �θ��� �Լ�
    }

    public void GameQuit()//���� ��ư�� ����� ����ϴ� �Լ�
    {
        Application.Quit();
    }

    void Update()
    {
        if (!isLive)
            return;// �� ��ũ��Ʈ�� Update �迭 ������ ���� �߰��ϱ�

        gameTime += Time.deltaTime;
        // Ÿ�̸� �������� deltaTime�� ��� ���ϱ�. deltaTime : 1�������� �Һ��� �ð�.
        if (gameTime > maxGameTime)// Ÿ�̸Ӱ� ���� �ð� ���� �����ϸ� ��ȯ�ϵ��� �ۼ�
        {
            gameTime = maxGameTime;
            GameVictroy();
        }
        

    }

    

    public void GetExp() // ����ġ ���� �Լ�
    {
        if(!isLive)
            return;
        exp++;

        if(exp == nextExp[Mathf.Min(level, nextExp.Length-1)]) // �������� �ʿ� ����ġ�� �����ϸ� ���� ���ϵ��� ����
        // ���� nextExp[level]���� Mathf�� Ȱ���Ͽ� ������ �������� �����ϰ� ��.
        // Min�Լ��� ����Ͽ� �ְ� ����ġ�� �״�� ����ϵ��� ����
        {
            level++;
            exp = 0;
            uiLevelUp.Show();
            // ���ӸŴ����� ���� �� ������ â�� �����ִ� �Լ� ȣ��
        }
    }

  

    public void Stop()// ������ �� �ð� ������ ����ϴ� �Լ�
    {
        isLive = false;
        Time.timeScale = 0;
        // timeScale : ����Ƽ�� �ð� �ӵ�(����)
        uiJoy.localScale = Vector3.zero;
    }


    public void Resume()// �ɷ� ���� �� �ٽ� �۵���Ű�� �Լ�
    {
        isLive = true;
        Time.timeScale = 1;
        uiJoy.localScale = Vector3.one;
    }


}
