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
            Vector2 pos = new Vector2(Random.Range(-8f, 8f), Random.Range(-4f, 4f));
            Sg.CreateSlime(type, pos);
            YandexGame.SaveProgress();

        }
        else
        {
            GameCanvas.instance.canvasNotMoney.SetActive(true);
        }
    }
}
