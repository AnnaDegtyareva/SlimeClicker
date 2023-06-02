using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Buy : MonoBehaviour
{
    public int price;
    public int type;
    public Sprite img;

    [SerializeField] TextMeshProUGUI textPrice;
    [SerializeField] Image imgPrefab;

    [SerializeField] GameObject shopCanvas;

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
            shopCanvas.SetActive(false);
            GameObject newSlime = Instantiate(SlimeGenerator.Instance.slimePrefabs[type], new Vector2(0, 0), Quaternion.identity);

        }
    }
}
