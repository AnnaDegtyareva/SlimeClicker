using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class MenuCanvas : MonoBehaviour
{
    public static MenuCanvas Instance;
    [SerializeField] public GameObject canvasMenu;
    [SerializeField] public GameObject canvasCollection;
    [SerializeField] GameObject prefab;
    [SerializeField] Transform grid;
    [SerializeField] public Transform canvasInfo;
    public List<GameObject> butonnsCollection = new List<GameObject>();

    [SerializeField] GameObject[] slimePrefabs;
    [SerializeField] Sprite[] slimeImg;

    [SerializeField] GameObject canvasNotSlime;

    [SerializeField] AudioSource music;

    [SerializeField] GameObject musicOn;
    [SerializeField] GameObject musicOff;

    [SerializeField] GameObject soundsOn;
    [SerializeField] GameObject soundsOff;

    [SerializeField] public GameObject prefabInfo;

    [SerializeField] public string[] slimeInfoRu;
    [SerializeField] public string[] slimeInfoEn;
    [SerializeField] public Sprite[] slimeInfoImg;

    private void Awake()
    {
        Instance = this;

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
            music.Play();
            musicOn.SetActive(true);
            musicOff.SetActive(false);
        }
        else
        {
            music.Stop();
            musicOn.SetActive(false);
            musicOff.SetActive(true);
        }
        if (YandexGame.savesData.sounds)
        {
            soundsOn.SetActive(true);
            soundsOff.SetActive(false);
        }
        else
        {
            soundsOn.SetActive(false);
            soundsOff.SetActive(true);
        }
    }

    public void Music()
    {
        if (YandexGame.savesData.music)
        {
            music.Stop();
            YandexGame.savesData.music = false;
            musicOn.SetActive(false);
            musicOff.SetActive(true);
        }
        else
        {
            music.Play();
            YandexGame.savesData.music = true;
            musicOn.SetActive(true);
            musicOff.SetActive(false);
        }

        YandexGame.SaveProgress();
    }
    public void Sounds()
    {
        if (YandexGame.savesData.sounds)
        {
            YandexGame.savesData.sounds = false;
            soundsOn.SetActive(false);
            soundsOff.SetActive(true);
        }
        else
        {
            YandexGame.savesData.sounds = true;
            soundsOn.SetActive(true);
            soundsOff.SetActive(false);
        }

        YandexGame.SaveProgress();
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void ClearCollection()
    {
        for (int i = 0; i < butonnsCollection.Count; i++)
        {
            Destroy(butonnsCollection[i]);
        }
    }

    public void Collection()
    {
        ClearCollection();

        canvasMenu.SetActive(false);
        canvasCollection.SetActive(true);
        for (int i = 0; i < slimeImg.Length; i++)
        {
            if (YandexGame.savesData.slimeOpen[i] == true)
            {
                GameObject newButton = Instantiate(prefab, Vector3.zero, Quaternion.identity);
                newButton.transform.SetParent(grid, false);
                newButton.GetComponent<SlimeType>().nameRu = slimePrefabs[i].GetComponent<SlimeMove>().slimeNameRu;
                newButton.GetComponent<SlimeType>().nameEn = slimePrefabs[i].GetComponent<SlimeMove>().slimeNameEn;
                newButton.GetComponent<SlimeType>().img = slimeImg[i];
                newButton.GetComponent<SlimeType>().index = i;

                butonnsCollection.Add(newButton);
            }
        }

        if(butonnsCollection.Count == 0)
        {
            canvasNotSlime.SetActive(true);
        }
    }
}
