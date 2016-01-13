using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using RunOut.Utils;

public class SongButton : MonoBehaviour
{
    
    public MusicFromDeviceDataModel song;
    public event SongSelectedHandler SongSelected;
    public Text SongText;

    private void Start()
    {
        this.SongText.text = song.SongName;
    }

    public void OnSongSelected()
    {
        if (this.SongSelected != null)
        {
            this.SongSelected(new SongSelectedEventArgs { Song = this.song}, this);
        }
    }


	
	
}


