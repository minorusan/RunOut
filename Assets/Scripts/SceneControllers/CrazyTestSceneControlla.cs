using UnityEngine;
using System.Collections;
using AssemblyCSharp;
using UnityEngine.UI;
using System.Linq;

public class CrazyTestSceneControlla : MonoBehaviour {

	public AudioSource someAoudio;
	public Text text;
	public Text lastError;
	public InputField input;
	private MusicModule musicModule;
	private AudioClip clip;

	// Use this for initialization
	void Start () {
	
	}

	void Update () 
	{
		
	}


	public void CheckOutDir()
	{
		if (musicModule == null) 
		{
			musicModule = new MusicModule ();
		} 
	
		lastError.text = musicModule.lastError;
		var resultString = "";
		foreach (var dir in musicModule.GetCurrentDirFolder(input.text))
		{
			resultString += dir.ToString() + "\n";
		}

		this.text.text = resultString;
	}

	public void CheckOutFiles()
	{
		if (musicModule == null) 
		{
			musicModule = new MusicModule ();
		} 

		lastError.text = musicModule.lastError;
		var audios = musicModule.GetAudioFilesInDir ();

		var resultString = "";
		if (audios != null && audios.Count > 0) {
			foreach (var dir in audios) {
				resultString += dir.SongName.ToString () + "\n";
			}

			someAoudio.Stop ();
			this.text.text = resultString;

			var songName = audios.FirstOrDefault ().FullPath;

			clip = string.IsNullOrEmpty (songName) ? null : new WWW ("file://" + songName).GetAudioClip (false, true);

			if (clip != null && !someAoudio.isPlaying) {
				someAoudio.clip = clip;
				someAoudio.Play ();
			}
		}
		else 
		{
			Debug.Log ("Error in finding music.");
		}
	}
}
