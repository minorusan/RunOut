using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using RunOut.Core.Utilities;

public class MenuSceneController : MonoBehaviour {

	public Animator controller;


	public void StartGame()
    {
		this.controller.SetTrigger ("NewGameTouch");
        //Application.LoadLevelAsync("Game");
    }

	public void LaunchNewGame()
	{
		SceneManager.LoadScene ("Game");
	}

	private void Update()
	{
		Tools.ManageGameObjects ();
	}

	private void OnDestroy()
	{
		Tools.ResetGameObjects ();
	}
}
