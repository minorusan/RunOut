using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using RunOut.Utils;
using System;

public class ScoreUpdater : MonoBehaviour {

    private Text text;

    private void Start()
    {
        this.text = this.GetComponent<Text>();
    }
	// Update is called once per frame
	void Update ()
    {
        this.text.text = Convert.ToInt32(GameStats.GetInstance().GainedScore).ToString();
	}
}
