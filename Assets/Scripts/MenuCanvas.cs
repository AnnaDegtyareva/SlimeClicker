using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class MenuCanvas : MonoBehaviour
{
    [SerializeField] public GameObject canvasMenu;
    [SerializeField] public GameObject canvasCollection;
    [SerializeField] GameObject prefab;
    [SerializeField] Transform grid;
    public List<GameObject> butonnsCollection = new List<GameObject>();

    [SerializeField] GameObject[] slimePrefabs;
    [SerializeField] Sprite[] slimeImg;

    [SerializeField] GameObject canvasNotSlime;

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
                newButton.GetComponent<SlimeType>().name = slimePrefabs[i].GetComponent<SlimeMove>().slimeName;
                newButton.GetComponent<SlimeType>().img = slimeImg[i];

                butonnsCollection.Add(newButton);
            }
        }

        if(butonnsCollection.Count == 0)
        {
            canvasNotSlime.SetActive(true);
        }
    }
}
