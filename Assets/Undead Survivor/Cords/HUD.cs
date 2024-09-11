using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;

public class HUD : MonoBehaviour
{
    public enum InfoType { Exp, Level, Kill, Time, Health }
    // �ٷ�� �� �����͸� �̸� ������ enum���� ����
    public InfoType type;
    // ������ �������� Ÿ������ ���� �߰�

    Text myText;
    // UI ������Ʈ�� ����� ���� UnityEngine.UI ���ӽ����̽� ���
    Slider mySlider;

    void Awake()
    {
        myText = GetComponent<Text>();
        mySlider = GetComponent<Slider>();
    }

    void LateUpdate()
    // LateUpdate���� switch ~ case ������ ���� ������
    {
        switch (type)
        {
            case InfoType.Exp :
                float curExp = GameManager.instance.exp;
                float maxExp = GameManager.instance.nextExp[Mathf.Min(GameManager.instance.level, GameManager.instance.nextExp.Length - 1)] ;
                mySlider.value = curExp / maxExp;
                // �����̴��� ������ �� : ���� ����ġ / �ִ� ����ġ
                break;
            case InfoType.Level:
                myText.text = string.Format("Lv.{0:F0}", GameManager.instance.level);
                // Format : �� ���� ���ڰ��� ������ ������ ���ڿ��� ������ִ� �Լ�
                // Format("Lv.{0}", GameManager.instance.level)
                // ->���� ���� ���ڿ��� �� �ڸ��� {����} ���·� �ۼ�
                // F0, F1, F2... : �Ҽ��� �ڸ��� ����
                break;
            case InfoType.Kill:
                myText.text = string.Format("{0:F0}", GameManager.instance.kill);
                break;
            case InfoType.Time:
                float remainTime = GameManager.instance.maxGameTime - GameManager.instance.gameTime;
                // �帣�� �ð��� �ƴ� ���� �ð����� ���ϱ�
                int min = Mathf.FloorToInt(remainTime / 60);
                // 60���� ������ ���� ���ϵ� FloorToInt�� �Ҽ��� ������
                int sec = Mathf.FloorToInt(remainTime % 60);
                // A % B : A�� B�� ������ ���� ������
                myText.text = string.Format("{0:D2}:{1:D2}", min, sec);
                // D0, D1, D2... : �ڸ� ���� ����
                break;
            case InfoType.Health:
                float curHealth = GameManager.instance.health;
                float maxHealth = GameManager.instance.maxHealth;
                mySlider.value = curHealth / maxHealth;
                break;
            //default:

            //    break;
        }
    }


}
