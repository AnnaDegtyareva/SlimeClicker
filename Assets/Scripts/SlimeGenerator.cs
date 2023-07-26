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
    public int index;

    [SerializeField] public GameObject[] slimePrefabs;
    [SerializeField] public Sprite[] slimeImg;
    [SerializeField] public GameObject slimeText;

    public int money;
    [SerializeField] public TextMeshProUGUI moneyText;

    [SerializeField] public GameObject prefabFlyText;
    [SerializeField] public GameObject canvas;

    [SerializeField] public GameObject[] world;
    [SerializeField] public Sprite[] worldImg;

    public List<GameObject> allSlimesNow = new List<GameObject>();

    public int startCount = 0;

    [SerializeField] public GameObject[] prefabsFood;
    [SerializeField] public GameObject money_canvas;

    private void Awake()
    {
        Instance = this;
    }

    public void textUpd()
    {
        YandexGame.savesData.money = money;
        moneyText.text = money.ToString();
        YandexGame.SaveProgress();
    }

    public void CreateText(int countMoney, Vector2 pos)
    {
        GameObject newText = Instantiate(prefabFlyText, prefabFlyText.transform.position = pos, Quaternion.identity) as GameObject;
        newText.transform.SetParent(money_canvas.transform, false);
        newText.GetComponent<TextMeshProUGUI>().text = countMoney.ToString();
        newText.GetComponent<Rigidbody2D>().velocity = pos / 30;

        money += countMoney;
        textUpd();

    }

    private void Start()
    {
        width = Screen.width;
        height = Screen.height;

        money = YandexGame.savesData.money;
        textUpd();

        InvokeRepeating("Slime", 5, 20);//поменять цифры

        ChangeWorld();


        if (YandexGame.savesData.FirstLaunch == true)
        {
            for (int i = 0; i < 2; i++)
            {
                Vector2 pos = new Vector2(Random.Range(-8f, 8f), Random.Range(-4f, 4f));
                CreateSlime(0, pos);
            }
            slimeText.SetActive(true);
            YandexGame.savesData.slimeOpen[0] = true;
            YandexGame.savesData.FirstLaunch = false;
        }
        else
        {
            for (int i = 0; i < YandexGame.savesData.allSlimes.Length; i++)
            {
                if (YandexGame.savesData.allSlimes[i] != 0)
                {
                    for (int j = 0;  j < YandexGame.savesData.allSlimes[i];  j++)
                    {
                        LoadSlime(i);
                    }
                }
            }
        }

        YandexGame.SaveProgress();

        
    }

    public void LoadSlime(int type)
    {
        GameObject newSlime = Instantiate(slimePrefabs[type], new Vector2(Random.Range(-8f, 8f), Random.Range(-4f, 4f)), Quaternion.identity);
        newSlime.GetComponent<SlimeMove>().index = index;
        index++;
        allSlimesNow.Add(newSlime);
    }

    public void Slime()
    {
        Vector2 pos = new Vector2(Random.Range(-8f, 8f), Random.Range(-4f, 4f));
        CreateSlime(-1, pos);
    }

    public void CreateSlime(int type, Vector2 pos)
    {
        int count = 0;

        for (int i = 0; i < YandexGame.savesData.slimeOpen.Length; i++)
        {
            if (YandexGame.savesData.slimeOpen[i] == true)
            {
                count++;
            }
        }

        if(startCount != 0)
        {
            startCount = YandexGame.savesData.world * 4;
        }

        if(type == -1)
        {
            type = Random.Range(startCount, count-1);
        }
        else if (type == -2)
        {
            type = Random.Range(startCount, count - 2);
        }

        GameObject newSlime = Instantiate(slimePrefabs[type], pos, Quaternion.identity);
        newSlime.GetComponent<SlimeMove>().index = index;
        YandexGame.savesData.allSlimes[type]++;
        index++;
        YandexGame.SaveProgress();
        allSlimesNow.Add(newSlime);

        if (YandexGame.savesData.sounds)
        {
            GameCanvas.instance.slimeAudio.clip = GameCanvas.instance.slimeAudios[0];
            GameCanvas.instance.slimeAudio.Play();
        }

    }

    public bool newSlime(int i, Vector3 fp, Vector3 sp)
    {
        if (i + 1 == slimePrefabs.Length) return false;

        GameObject newSlime = Instantiate(slimePrefabs[i+1], (fp+sp)/2, Quaternion.identity);
        SlimeMove sm = newSlime.GetComponent<SlimeMove>();
        sm.index = index;
        YandexGame.savesData.allSlimes[i+1]++;
        YandexGame.savesData.allSlimes[i] -= 2;
        index++;

        if (YandexGame.savesData.slimeOpen[i+1] == false)
        {
            slimeText.SetActive(true);
            YandexGame.savesData.slimeOpen[i+1] = true;
        }

        CreateText(25, Input.mousePosition);

        if((i+1)%4 == 0)
        {
            Debug.Log(YandexGame.savesData.world);
            YandexGame.savesData.world++;
            ChangeWorld();
        }

        allSlimesNow.Add(newSlime);

        if (YandexGame.savesData.sounds)
        {
            GameCanvas.instance.slimeAudio.clip = GameCanvas.instance.slimeAudios[1];
            GameCanvas.instance.slimeAudio.Play();
        }

        YandexGame.savesData.foodPrice += sm.slimeMoney;
        GameCanvas.instance.ChangeFoodPrice();


        YandexGame.SaveProgress();

        return true;
    }

    public void ChangeWorld()
    {
        int number = YandexGame.savesData.world;

        for (int i = 0; i < world.Length; i++)
        {
            world[i].GetComponent<SpriteRenderer>().sprite = worldImg[number];
        }

        for(int i = 0; i < allSlimesNow.Count; i++)
        {
            Destroy(allSlimesNow[i]);
        }
        for (int j = 0; j < number*4; j++)
        {
            YandexGame.savesData.allSlimes[j] = 0;
        }

        startCount = number*4;

    }
}
