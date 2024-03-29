using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;
using UnityEngine.SceneManagement;
using TMPro;

public class GameCanvas : MonoBehaviour
{
    public static GameCanvas instance;


    [SerializeField] public GameObject canvasShop;
    [SerializeField] GameObject prefab;
    [SerializeField] Transform grid;

    [SerializeField] public GameObject canvasNotMoney;

    public List<GameObject> butonnsShop = new List<GameObject>();

    [SerializeField] public AudioSource worldAudio;
    [SerializeField] public AudioSource slimeAudio;

    [SerializeField] public AudioClip[] slimeAudios;

    [SerializeField] public TextMeshProUGUI textFoodPrice;
    public int foodPrice;

    [SerializeField] GameObject pauseOn;
    [SerializeField] GameObject pauseOff;
    [SerializeField] GameObject pauseCanvas;
    bool pause = false;

    [SerializeField] GameObject[] textPrize;
    [SerializeField] GameObject PrizeCanvas;
    [SerializeField] GameObject psPrize;
    [SerializeField] TextMeshProUGUI textMoney;

    public RectTransform Ui1;
    public RectTransform Ui2;
    public Vector3 pos1;
    public Vector3 pos2;

    private void Start()
    {
        pos1 = Camera.main.ScreenToWorldPoint(Ui1.position);
        pos2 = Camera.main.ScreenToWorldPoint(Ui2.position);
    }

    private void Awake()
    {
        instance = this;

        if (YandexGame.SDKEnabled)
        {
            Load();
        }
        else
        {
            YandexGame.GetDataEvent += Load;
        }
    }

    public void Load()
    {
        if (YandexGame.savesData.music)
        {
            worldAudio.Play();
        }
        foodPrice = YandexGame.savesData.foodPrice;
        ChangeFoodPrice();

        pauseOn.SetActive(false);
        pauseOff.SetActive(true);
        pauseCanvas.SetActive(false);
        PrizeCanvas.SetActive(false);
        for (int i = 0; i < textPrize.Length; i++)
        {
            textPrize[i].SetActive(false);
        }
        Time.timeScale = 1f;
    }

    public void SlimeAds()
    {
        YandexGame.RewVideoShow(0);
    }

    public void Prize()
    {
        int prize = Random.Range(0, 3);
        if (prize == 0)
        {
            Vector2 pos = new Vector2(Random.Range(-8f, 8f), Random.Range(-4f, 4f));
            SlimeGenerator.Instance.CreateSlime(-2, pos);
            GameObject ps = Instantiate(psPrize, new Vector2(pos.x, pos.y), Quaternion.identity);
        }
        else if (prize == 1)
        {
            GameObject newFood = Instantiate(SlimeGenerator.Instance.prefabsFood[Random.Range(0, SlimeGenerator.Instance.prefabsFood.Length)],
                new Vector2(Random.Range(-8f, 8f), Random.Range(-4f, 4f)), Quaternion.identity);
            GameObject ps = Instantiate(psPrize, new Vector2(newFood.transform.position.x, newFood.transform.position.y), Quaternion.identity);
            ps.transform.SetParent(newFood.transform);
        }
        else
        {
            int money = Random.Range(10, 350);
            SlimeGenerator.Instance.money += money;
            SlimeGenerator.Instance.textUpd();
            textMoney.text = money.ToString();
        }
        PrizeCanvas.SetActive(true);
        textPrize[prize].SetActive(true);
    }

    public void Pause()
    {
        if (!pause)
        {
            pause = true; 
            pauseOn.SetActive(true);
            pauseOff.SetActive(false);
            pauseCanvas.SetActive(true);
            ViewingAdsYG.Instance.Pause(true);
            Time.timeScale = 0f;
        }
        else
        {
            pause = false;
            pauseOn.SetActive(false);
            pauseOff.SetActive(true);
            pauseCanvas.SetActive(false);
            ViewingAdsYG.Instance.Pause(false);
            Time.timeScale = 1f;
        }
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ClearShop()
    {
        for (int i = 0; i < butonnsShop.Count; i++)
        {
            Destroy(butonnsShop[i]);
        }
    }
    public void OpenShop()
    {
        ClearShop();

        canvasShop.SetActive(true);
        for (int i = SlimeGenerator.Instance.startCount; i < SlimeGenerator.Instance.slimeImg.Length; i++)
        {
            if (YandexGame.savesData.slimeOpen[i] == true)
            {
                GameObject newButton = Instantiate(prefab, Vector3.zero, Quaternion.identity);
                newButton.transform.SetParent(grid, false);
                newButton.GetComponent<Buy>().type = i;
                newButton.GetComponent<Buy>().price = SlimeGenerator.Instance.slimePrefabs[i].GetComponent<SlimeMove>().slimePrice;
                newButton.GetComponent<Buy>().img = SlimeGenerator.Instance.slimeImg[i];

                butonnsShop.Add(newButton);
            }
        }
    }
    public void ChangeFoodPrice()
    {
        foodPrice = YandexGame.savesData.foodPrice;
        textFoodPrice.text = foodPrice.ToString();
    }

    public void CreateFood()
    {
        if (SlimeGenerator.Instance.money >= foodPrice)
        {
            GameObject newFood = Instantiate(SlimeGenerator.Instance.prefabsFood[Random.Range(0, SlimeGenerator.Instance.prefabsFood.Length)], 
                new Vector2(Random.Range(-8f, 8f), Random.Range(-4f, 4f)), Quaternion.identity);
            SlimeGenerator.Instance.money -= foodPrice;
            SlimeGenerator.Instance.textUpd();
        }
        else
        {
            canvasNotMoney.SetActive(true);
        }
    }

    public void CleanerWorld()
    {
        for (int i = 0; i < YandexGame.savesData.allSlimes.Length; i++)
        {
            if (YandexGame.savesData.allSlimes[i] != 0)
            {
                YandexGame.savesData.allSlimes[i] = 0;
            }
        }
        for (int j = 0; j < SlimeGenerator.Instance.allSlimesNow.Count; j++)
        {
            Destroy(SlimeGenerator.Instance.allSlimesNow[j]);
        }
    }

    public void Exit(GameObject canvasNow)
    {
        canvasNow.SetActive(false);
    }
}
