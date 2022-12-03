using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RuntimeSceneManager : MonoBehaviour
{
    public const int MainGameIndex = 1;
    public const int MenuSceneIndex = 0;

    public void SwitchToMainMenuScene()
    {
        SwitchToMainMenuScene(0);
    }

    public void SwitchToGameScene()
    {
       SwitchToGameScene(0);
    }
    private void SwitchToGameScene(float loadSceneDelay)
    {
        StartCoroutine(WaitAndLoadScene(loadSceneDelay, MainGameIndex));
    }

    private void SwitchToMainMenuScene(float loadSceneDelay, bool additive = false)
    {
        StartCoroutine(WaitAndLoadScene(loadSceneDelay, MenuSceneIndex, additive));
    }

    IEnumerator WaitAndLoadScene(float secs, int sceneIndex, bool additive=false)
    {
        yield return new WaitForSeconds(secs);
        SceneManager.LoadScene(sceneIndex, additive? LoadSceneMode.Additive : LoadSceneMode.Single);
    }
}
