using UnityEngine;
using System.Collections;

public class MenuSceneController : MonoBehaviour {

    public GameObject gamePanel;

	public void StartGame()
    {
        this.gamePanel.SetActive(true);
        //Application.LoadLevelAsync("Game");
    }
}
