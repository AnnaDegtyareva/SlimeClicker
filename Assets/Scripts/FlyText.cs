using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FlyText : MonoBehaviour
{
    bool move;
    private void Update()
    {
        if (!move) return;
    }

    public void StartFly(int score)
    {
        GetComponent<TextMeshProUGUI>().text = score.ToString();
        move = true;

    }
}
