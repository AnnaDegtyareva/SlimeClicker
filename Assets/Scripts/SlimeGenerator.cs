using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlimeGenerator : MonoBehaviour
{
    [SerializeField] GameObject prefab;

    [SerializeField] List<GameObject> allSlimes = new List<GameObject>();

    int width;//ширина
    int height;//высота

    private void Start()
    {
        width = Screen.width;
        height = Screen.height;
                        
        InvokeRepeating("CreateSlime", 15, 15);
        for (int i = 0; i < 2; i++)
        {
            CreateSlime();
        }
    }

    public void CreateSlime()
    {
        GameObject newSlime = Instantiate(prefab, new Vector2 (Random.Range(-8f, 8f), Random.Range(-4f, 4f)), Quaternion.identity);
        allSlimes.Add(newSlime);
    }
}
