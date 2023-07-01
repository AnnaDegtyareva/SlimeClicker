using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using YG;

public class SlimeType : MonoBehaviour
{
    public string nameRu;
    public string nameEn;
    public Sprite img;

    [SerializeField] TextMeshProUGUI textName;
    [SerializeField] Image imgPrefab;

    public int index;

    private void Start()
    {
        if(YandexGame.EnvironmentData.language == "ru")
        {
            textName.text = nameRu.ToString();
        }
        else
        {
            textName.text = nameEn.ToString();
        }
        imgPrefab.sprite = img;
    }

    //public void OpenInfo()
    //{
    //    GameObject newImg = Instantiate(MenuCanvas.Instance.prefabInfo, Vector3.zero, Quaternion.identity);
    //    newImg.transform.SetParent(MenuCanvas.Instance.canvasInfo, false);
    //    newImg.GetComponent<SlimeInfo>().textRu = MenuCanvas.Instance.slimeInfoRu[index];
    //    newImg.GetComponent<SlimeInfo>().textEn = MenuCanvas.Instance.slimeInfoEn[index];
    //    newImg.GetComponent<SlimeInfo>().img = MenuCanvas.Instance.slimeInfoImg[index];
    //}
}
