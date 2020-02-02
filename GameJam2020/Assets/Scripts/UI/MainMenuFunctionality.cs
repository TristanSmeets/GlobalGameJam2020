using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class MainMenuFunctionality : MonoBehaviour
{
    [SerializeField] VideoPlayer videoPlayer = null;
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
        videoPlayer.Play();
        StartCoroutine(LoadSceneAfterVideo(scene));
    }

    private IEnumerator LoadSceneAfterVideo(string sceneName)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
        async.allowSceneActivation = false;
        if(Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }

        while(!async.isDone)
        {
            if(async.progress>=0.9f)
            {
                if(!videoPlayer.isPlaying)
                {
                    async.allowSceneActivation = true;
                }
            }
            yield return null;
        }
    }
}
