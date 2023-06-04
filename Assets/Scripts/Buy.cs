using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using YG;

public class Buy : MonoBehaviour
{
    public int price;
    public int type;
    public Sprite img;

    [SerializeField] TextMeshProUGUI textPrice;
    [SerializeField] Image imgPrefab;

    SlimeGenerator Sg = SlimeGenerator.Instance;

    private void Start()
    {
        textPrice.text = price.ToString();
        imgPrefab.sprite = img;
    }

    public void BuySlime()
    {
        if(price <= Sg.money)
        {
            Sg.money -= price;
            Sg.textUpd();
            GameCanvas.instance.canvasShop.SetActive(false);
            Sg.CreateSlime(type);
            YandexGame.SaveProgress();

        }
    }
}
