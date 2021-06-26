using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneScript : MonoBehaviour
{
    public void LoadScene(int sceneNumber)
    {
        StartCoroutine(LoadSceneCoroutine(sceneNumber));
    }

    IEnumerator LoadSceneCoroutine(int sceneNumber)
    {
        yield return new WaitForSecondsRealtime(0.2f);
        SceneManager.LoadScene(sceneNumber);
    }

}
