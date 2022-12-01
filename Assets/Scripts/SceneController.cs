using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public void SwitchToMainMenuScene()
    {
        GameManager.Instance.SwitchToMainMenuScene(0);
    }
}
