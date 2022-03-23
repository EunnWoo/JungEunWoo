using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ShotMessage
{
    public string msg;
    public float time;
    public ShotMessage(string _msg, float _time)
    {
        msg     = _msg;
        time    = _time;
    }
}

public class UI_ShotMessage : MonoBehaviour
{
    #region sigletone
    bool bInit;
    public static UI_ShotMessage ins;
    private void Awake()
    {
        ins = this;
    }
    #endregion


    public List<ShotMessage> list = new List<ShotMessage>();
    public Text text;
    public GameObject body;
    public float duration = 3f;
    void Start()
    {
        body.SetActive(false);
    }

    public void SetMessage(string _msg)
    {
        list.Add(new ShotMessage(_msg, Time.time + duration*2f));

        body.SetActive(true);
        text.text = "";
        for (int i = 0, imax = list.Count; i < imax; i++)
        {
            if(list[i] != null && Time.time < list[i].time)
            {
                text.text += list[i].msg + "\n";
            }            
        }

        StopCoroutine("Co_HiddenMessage");
        StartCoroutine("Co_HiddenMessage", duration);
    }

    IEnumerator Co_HiddenMessage(float _duration)
    {
        yield return new WaitForSeconds(_duration);

        float _speed = 1f;
        float _percent = 0;
        Color _startColor = new Color(0f, 0f, 0f, 1f);
        Color _endColor   = new Color(0f, 0f, 0f, 0f);
        while (_percent < 1f)
        {
            _percent += _speed * Time.deltaTime;
            text.color = Color.Lerp(_startColor, _endColor, _percent);
            yield return null;
        }
        text.color = _startColor;
        body.SetActive(false);

        for (int i = list.Count - 1; i >= 0; i--)
        {
            if(list[i] != null && Time.time > list[i].time)
            {
                list.RemoveAt(i);
            }
        }
    }
}
