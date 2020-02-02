using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoading : MonoBehaviour
{
   public void LoadScene(string sceneName)
    {
        StopAllCoroutines();
        StartCoroutine(StartLoading(sceneName));
    }

    private IEnumerator StartLoading(string sceneName)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);

        asyncOperation.allowSceneActivation = true;

        if(Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }

        while (!asyncOperation.isDone)
        {
        }
            yield return null;
    }
}
