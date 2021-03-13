using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kodilla.Module8.Scripts;

public class FadeRemoteControl : MonoBehaviour
{
    public FadingImage fading;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            fading.DoFade();
        }
    }
}
