using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Game : MonoBehaviour
{
    public static Game instance;

    public int money;
    [SerializeField] public TextMeshProUGUI moneyText;
    [SerializeField] public GameObject prefabFlyText;

    private void Awake()
    {
        instance = this;
    }

    public void textUpd()
    {
        moneyText.text = money.ToString();
    }
}
