using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class GameCanvas : MonoBehaviour
{
    public static GameCanvas instance;

    [SerializeField] public GameObject canvasShop;
    [SerializeField] GameObject prefab;
    [SerializeField] Transform grid;

    public List<GameObject> butonnsShop = new List<GameObject>();

    private void Awake()
    {
        instance = this;
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
        for (int i = 0; i < SlimeGenerator.Instance.slimeImg.Length; i++)
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

    public void Exit(GameObject canvasNow)
    {
        canvasNow.SetActive(false);
    }
}
