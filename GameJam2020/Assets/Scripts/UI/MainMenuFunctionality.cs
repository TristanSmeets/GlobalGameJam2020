using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class MainMenuFunctionality : MonoBehaviour
{
    [SerializeField] private string scene = string.Empty;
    private bool startedGame = false;

    public void CloseApplication()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        if(startedGame)
        {
            return;
        }
        SceneManager.LoadScene(scene);
    }
}
