using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class SlimeGenerator : MonoBehaviour
{
    public static SlimeGenerator Instance = new SlimeGenerator();


    int width;//ширина
    int height;//высота
    private int index;

    [SerializeField] public GameObject[] slimePrefabs;
    [SerializeField] public GameObject[] slimeText;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        width = Screen.width;
        height = Screen.height;
                        
        InvokeRepeating("CreateSlime", 3, 3);
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
        index++;
        YandexGame.SaveProgress();
    }

    public bool newSlime(int i, Vector3 fp, Vector3 sp)
    {
        if (i + 1 == slimePrefabs.Length) return false;

        GameObject newSlime = Instantiate(slimePrefabs[i+1], (fp+sp)/2, Quaternion.identity); 
        newSlime.GetComponent<SlimeMove>().index = index;
        index++;
        YandexGame.savesData.count[i] -= 2;
        YandexGame.savesData.count[i+1]++;

        if (YandexGame.savesData.slimeOpen[i] == false)
        {
            slimeText[i].SetActive(true);
            YandexGame.savesData.slimeOpen[i] = true;
        }

        YandexGame.SaveProgress();
        return true;
    }
}
