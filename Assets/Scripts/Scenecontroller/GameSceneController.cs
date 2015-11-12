using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameSceneController : MonoBehaviour {

    private static List<Meteor> meteors = new List<Meteor>();

	// Use this for initialization
	void Start () {
	    
	}

    public static void AddMeteorToList(Meteor meteor)
    {
        meteors.Add(meteor);
    }


	// Update is called once per frame
	void Update ()
    {
        foreach (var meteor in meteors.Where(m=>!m.gameObject.activeSelf))
        {
            meteor.gameObject.SetActive(true);
            meteor.Reset();
        }
	}
}
