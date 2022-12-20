using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void OnePlayerScene()
    {
        SceneManager.LoadScene("1PlayerScene", LoadSceneMode.Single);
    }

    public void TwoPlayerScene()
    {
        SceneManager.LoadScene("2PlayerScene", LoadSceneMode.Single);
    }

}
