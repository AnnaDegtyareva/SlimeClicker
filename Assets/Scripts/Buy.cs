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

    private void Start()
    {
        textPrice.text = price.ToString();
        imgPrefab.sprite = img;
    }

    public void BuySlime()
    {
        if(price <= SlimeGenerator.Instance.money)
        {
            SlimeGenerator.Instance.money -= price;
            SlimeGenerator.Instance.textUpd();
            GameCanvas.instance.canvasShop.SetActive(false);
            GameObject newSlime = Instantiate(SlimeGenerator.Instance.slimePrefabs[type], new Vector2(Random.Range(-8f, 8f), Random.Range(-4f, 4f)), Quaternion.identity);
            YandexGame.savesData.allSlimes[type]++;
            YandexGame.SaveProgress();

        }
    }
}
