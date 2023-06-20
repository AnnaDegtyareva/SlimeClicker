using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SlimeType : MonoBehaviour
{
    public string name;
    public Sprite img;

    [SerializeField] TextMeshProUGUI textName;
    [SerializeField] Image imgPrefab;

    private void Start()
    {
        textName.text = name.ToString();
        imgPrefab.sprite = img;
    }
}
