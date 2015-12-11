using UnityEngine;
using System.Collections;

public class MenuSceneController : MonoBehaviour {

	public void StartGame()
    {
        Application.LoadLevelAsync("Game");
    }
}
