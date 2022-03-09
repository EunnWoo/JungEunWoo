using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Message : MonoBehaviour
{
    #region sigletone
    public static UI_Message ins;
    private void Awake()
    {
        ins = this;
    }
    #endregion
    public GameObject body;
    public Text title, content;
    System.Action on;
    void Start()
    {
        body.SetActive(false);
    }

    public void ShowMessage(string _title, string _content, System.Action _on = null)
    {
        body.SetActive(true);
        title.text = _title; //타이틀은 타이틀에넣어준다
        content.text = _content;

        on = _on; 
    }

    public void Invoke_OK()
    {
        body.SetActive(false);
        if(on != null)
        {
            on();
            on = null;
        }
    }

}
