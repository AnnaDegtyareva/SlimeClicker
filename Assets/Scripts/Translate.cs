using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;
using TMPro;
using UnityEngine.UI;

public class Translate : MonoBehaviour
{
    public string Ru;
    public string En;
    private void OnEnable()
    {
        if (YandexGame.SDKEnabled)
        {
            TranslateLang();
        }
        YandexGame.GetDataEvent += TranslateLang;
        
    }
    private void OnDisable()
    {
        YandexGame.GetDataEvent -= TranslateLang;
    }

    public void TranslateLang()
    {
        Text _text = GetComponent<Text>();
        TMP_Text tMP_ = GetComponent<TMP_Text>();
        if(YandexGame.EnvironmentData.language == "ru")
        {
            if(tMP_ != null)
            {
                tMP_.text = Ru;
            }
            if(_text != null)
            {
                _text.text = Ru;
            }
        }
        else
        {
            if (tMP_ != null)
            {
                tMP_.text = En;
            }
            if (_text != null)
            {
                _text.text = En;
            }
        }
    }
}
