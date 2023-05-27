using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FlyText : MonoBehaviour
{
    private void Start()
    {
        Invoke("Delete", 2f);
    }
    public void Delete()
    {
        Destroy(gameObject);
    }
}
