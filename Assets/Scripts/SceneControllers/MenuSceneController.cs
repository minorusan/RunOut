using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using RunOut.Core.Utilities;
using RunOut.Utils;

public class MenuSceneController : MonoBehaviour
{ 
    public Animator controller;
    
    public SongButton songButtonPrefab;
    public GameObject panelWithButton;

    public void StartGame()
    {
        this.controller.SetTrigger("NewGameTouch");
        this.PopulateList();
    }

    public void Start()
    {
       
    }

    private void PopulateList()
    {
        var module = new MusicModule();
        string startingDirectory = "";

#if UNITY_EDITOR
        startingDirectory = "";
#elif UNITY_ANDROID
        startingDirectory = "/sdcard";
#endif
        module.GetCurrentDirFolder(startingDirectory);
        var audios = module.GetAudioFilesInDir();

        foreach (var song in audios)
        {
            var songButton = Instantiate(songButtonPrefab);
            songButton.song = song;
            songButton.SongSelected += onSongSelected;
            songButton.transform.SetParent(this.panelWithButton.transform, false);
        }
    }

   

    private void onSongSelected(SongSelectedEventArgs args, object sender)
    {
        Tools.SelectedSong = args.Song;
        SceneManager.LoadScene(Strings.kSceneNameGame);
    }

    public void LaunchNewGame()
    {
        this.controller.SetTrigger("MusicSelection");
       
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
