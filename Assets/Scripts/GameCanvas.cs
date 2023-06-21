using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;
using UnityEngine.SceneManagement;

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

    public void CreateFood()
    {
        if (SlimeGenerator.Instance.money >= 25)
        {
            GameObject newFood = Instantiate(SlimeGenerator.Instance.prefabsFood[Random.Range(0, SlimeGenerator.Instance.prefabsFood.Length)], 
                new Vector2(Random.Range(-8f, 8f), Random.Range(-4f, 4f)), Quaternion.identity);
            SlimeGenerator.Instance.money -= 25;
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
