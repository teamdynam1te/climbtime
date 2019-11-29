﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    public Image whiteFade;

    // Start is called before the first frame update
    void Start()
    {
        whiteFade.canvasRenderer.SetAlpha(1.0f);

        fadeOut();
    }

    // Update is called once per frame
    void fadeOut()
    {
        whiteFade.CrossFadeAlpha(0, 2, false);
    }
}
