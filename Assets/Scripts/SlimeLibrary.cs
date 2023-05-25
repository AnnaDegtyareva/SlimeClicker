using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeLibrary : MonoBehaviour
{
    public static SlimeLibrary instance;

    private void Awake()
    {
        instance = this;
    }
}
