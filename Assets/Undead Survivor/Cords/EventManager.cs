using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    public Button btn_Start;
    public Button btn_Store;
    public Button btn_Collection;
    public Button btn_Option;
    public Button btn_Exit;

    public GameObject CharacterGroup;

    void Start()
    {
        CharacterGroup.SetActive(false);
    }

    public void ButtonClick()
    {
        IPointerDownHandler();
    }

    public void IPointerDownHandler()
    {
        //Debug.Log("Ω√¿€");
        CharacterGroup.SetActive(true);
        Application.Quit();
        btn_Start.gameObject.SetActive(false);
        btn_Store.gameObject.SetActive(false);
        btn_Collection.gameObject.SetActive(false);
        btn_Option.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
