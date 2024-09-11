using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;

public class HUD : MonoBehaviour
{
    public enum InfoType { Exp, Level, Kill, Time, Health }
    // 다루게 될 데이터를 미리 열거형 enum으로 선언
    public InfoType type;
    // 선언한 열거형을 타입으로 변수 추가

    Text myText;
    // UI 컴포넌트를 사용할 때는 UnityEngine.UI 네임스페이스 사용
    Slider mySlider;

    void Awake()
    {
        myText = GetComponent<Text>();
        mySlider = GetComponent<Slider>();
    }

    void LateUpdate()
    // LateUpdate에서 switch ~ case 문으로 로직 나누기
    {
        switch (type)
        {
            case InfoType.Exp :
                float curExp = GameManager.instance.exp;
                float maxExp = GameManager.instance.nextExp[Mathf.Min(GameManager.instance.level, GameManager.instance.nextExp.Length - 1)] ;
                mySlider.value = curExp / maxExp;
                // 슬라이더에 적용할 값 : 현재 경험치 / 최대 경험치
                break;
            case InfoType.Level:
                myText.text = string.Format("Lv.{0:F0}", GameManager.instance.level);
                // Format : 각 숫자 인자값을 지정된 형태의 문자열로 만들어주는 함수
                // Format("Lv.{0}", GameManager.instance.level)
                // ->인자 값의 문자열이 들어갈 자리를 {순번} 형태로 작성
                // F0, F1, F2... : 소수점 자리를 지정
                break;
            case InfoType.Kill:
                myText.text = string.Format("{0:F0}", GameManager.instance.kill);
                break;
            case InfoType.Time:
                float remainTime = GameManager.instance.maxGameTime - GameManager.instance.gameTime;
                // 흐르는 시간이 아닌 남은 시간부터 구하기
                int min = Mathf.FloorToInt(remainTime / 60);
                // 60으로 나누어 분을 구하되 FloorToInt로 소수점 버리기
                int sec = Mathf.FloorToInt(remainTime % 60);
                // A % B : A를 B로 나누고 남은 나머지
                myText.text = string.Format("{0:D2}:{1:D2}", min, sec);
                // D0, D1, D2... : 자리 수를 지정
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
