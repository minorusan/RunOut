using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;
using RunOut.Utils;

public class CrazyTestSceneControlla : MonoBehaviour {

	public AudioSource someAoudio;
	public Text text;
	public Text lastError;
	public InputField input;
	private MusicModule musicModule;
	private AudioClip clip;

	// Use this for initialization


    private void LateUpdate()
    {
        if (clip != null && clip.loadState == AudioDataLoadState.Loaded && !someAoudio.isPlaying)
        {
            someAoudio.clip = clip;
            someAoudio.Play();
        }
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
        Debug.Log("Began to fetch audio");

		var audios = musicModule.GetAudioFilesInDir ();
        Debug.Log("Ended fetching audio");
        var resultString = "";
		if (audios != null && audios.Count > 0)
        {
			foreach (var dir in audios)
            {
				resultString += dir.SongName + "\n";
			}

			someAoudio.Stop ();
			this.text.text = resultString;

            var songName = audios.FirstOrDefault();//.FullPath;

			clip = string.IsNullOrEmpty (songName.FullPath) ? null : new WWW ("file://" + songName.FullPath).GetAudioClip (false, true);
		}
		else 
		{
			Debug.Log ("Error in finding music.");
		}
	}
}
