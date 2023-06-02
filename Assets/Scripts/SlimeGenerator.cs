using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;
using TMPro;

public class SlimeGenerator : MonoBehaviour
{
    public static SlimeGenerator Instance;

    int width;//ширина
    int height;//высота
    private int index;

    [SerializeField] public GameObject[] slimePrefabs;
    [SerializeField] public Sprite[] slimeImg;
    [SerializeField] public GameObject[] slimeText;

    public int money;
    [SerializeField] public TextMeshProUGUI moneyText;

    [SerializeField] public GameObject prefabFlyText;
    [SerializeField] public GameObject canvas;


    private void Awake()
    {
        Instance = this;
    }

    public void textUpd()
    {
        moneyText.text = money.ToString();
        YandexGame.savesData.money = money;
        YandexGame.SaveProgress();
    }

    public void CreateText(int countMoney, Vector3 pos)
    {
        GameObject newText = Instantiate(prefabFlyText, prefabFlyText.transform.position = pos, Quaternion.identity) as GameObject;
        newText.transform.SetParent(canvas.transform, false);
        newText.GetComponent<TextMeshProUGUI>().text = countMoney.ToString();
        newText.GetComponent<Rigidbody2D>().velocity = pos / 30;
    }

    private void Start()
    {
        width = Screen.width;
        height = Screen.height;
                        
        InvokeRepeating("CreateSlime", 3, 3);//поменять цифры

        if (YandexGame.savesData.slimeOpen[0] == false)
        {
            slimeText[0].SetActive(true);
            YandexGame.savesData.slimeOpen[0] = true;
        }

        for (int i = 0; i < 2; i++)
        {
            CreateSlime();
        }
    }

    public void CreateSlime()
    {
        int count = 0;

        for (int i = 0; i < YandexGame.savesData.slimeOpen.Length; i++)
        {
            if (YandexGame.savesData.slimeOpen[i] == true)
            {
                count++;
            }
        }
        
        int type = Random.Range(0, count);

        YandexGame.savesData.count[type]++;

        GameObject newSlime = Instantiate(slimePrefabs[type], new Vector2 (Random.Range(-8f, 8f), Random.Range(-4f, 4f)), Quaternion.identity);
        newSlime.GetComponent<SlimeMove>().index = index;
        YandexGame.savesData.allSlimes[type]++;
        index++;
        YandexGame.SaveProgress();
    }

    public bool newSlime(int i, Vector3 fp, Vector3 sp)
    {
        if (i + 1 == slimePrefabs.Length) return false;

        GameObject newSlime = Instantiate(slimePrefabs[i+1], (fp+sp)/2, Quaternion.identity);
        SlimeMove sm = newSlime.GetComponent<SlimeMove>();
        sm.index = index;
        YandexGame.savesData.allSlimes[i]++;
        index++;
        YandexGame.savesData.count[i] -= 2;
        YandexGame.savesData.count[i+1]++;

        if (YandexGame.savesData.slimeOpen[i+1] == false)
        {
            slimeText[i+1].SetActive(true);
            YandexGame.savesData.slimeOpen[i+1] = true;
        }

        CreateText(sm.slimePrice, (fp + sp) / 2);

        YandexGame.SaveProgress();
        return true;
    }
}
