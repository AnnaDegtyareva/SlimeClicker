using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using YG;


public class SlimeInfo : MonoBehaviour
{
    public string textRu;
    public string textEn;
    public Sprite img;

    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Image imgPrefab;

    private void Start()
    {
        if (YandexGame.EnvironmentData.language == "ru")
        {
            text.text = textRu.ToString();
        }
        else
        {
            text.text = textEn.ToString();
        }
        imgPrefab.sprite = img;
    }

    public void DeleteMe()
    {
        Destroy(gameObject);
    }
}
