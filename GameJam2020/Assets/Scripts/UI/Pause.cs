using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public static event Action PausingGame = delegate { };
    public static event Action ResumingGame = delegate { };

    private void OnEnable()
    {
        PausingGame();
        Time.timeScale = 0;
    }
    private void OnDisable()
    {
        Time.timeScale = 1;
        ResumingGame();
    }
}
