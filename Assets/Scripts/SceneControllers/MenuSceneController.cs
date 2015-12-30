using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using RunOut.Core.Utilities;
using RunOut.Utils;

public class MenuSceneController : MonoBehaviour
{ 
    public Animator controller;

    public void StartGame()
    {
        this.controller.SetTrigger("NewGameTouch");
    }

    public void LaunchNewGame()
    {
        SceneManager.LoadScene(Strings.kSceneNameGame);
    }

    private void Update()
    {
        Tools.ManageGameObjects();
    }

    private void OnDestroy()
    {
        Tools.ResetGameObjects();
    }
}
