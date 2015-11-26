using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using RunOut.Core.Controllers;

public class PlayerHealth : MonoBehaviour {
    private Text uitext;
	// Use this for initialization
	void Start () {
        this.uitext = this.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {

        this.uitext.text = string.Format("Player health:{0}", GameSceneController.playerStats.Health.ToString());

        if (GameSceneController.playerStats.Health > 20)
        {
            this.uitext.color = Color.green;
            return;
        }
        else if (GameSceneController.playerStats.Health > 10)
        {
            this.uitext.color = Color.yellow;
            return;
        }
        else if (GameSceneController.playerStats.Health > 0)
        {
            this.uitext.color = Color.red;
            return;
        }

    }
}
