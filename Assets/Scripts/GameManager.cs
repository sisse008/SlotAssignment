using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private static GameManager instance = null;

    public static GameManager Instance
    {
        get
        {
            if (instance)
                return instance;
            instance = FindObjectOfType<GameManager>();
            return instance;
        }
    }

    private void Awake()
    {
        if (instance && instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }


    public const int MainGameIndex = 1;
    public const int MenuSceneIndex = 0;

    public void SwitchToGameScene(float loadSceneDelay)
    {
        StartCoroutine(WaitAndLoadScene(loadSceneDelay, MainGameIndex));
    }

    public void SwitchToMainMenuScene(float loadSceneDelay)
    {
        StartCoroutine(WaitAndLoadScene(loadSceneDelay, MenuSceneIndex));
    }

    IEnumerator WaitAndLoadScene(float secs, int sceneIndex)
    {
        yield return new WaitForSeconds(secs);
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
    }
}
