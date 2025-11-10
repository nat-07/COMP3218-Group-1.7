using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainScreen : MonoBehaviour
{
    public void loadMainScreen()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void loadGame()
    {
        SceneManager.LoadScene("URP2DSceneTemplate");
    }
}
