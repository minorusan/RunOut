using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;
using RunOut.Utils;

namespace AssemblyCSharp
{
	public class MusicModule
	{
		#region Private
		private string _curPath ;
		private List<String> _curDirectoryFolderPaths = new List<String>() ;
		#endregion

		public List<MusicFromDeviceDataModel> audioFilesInDir;
		public string lastError;

		public List<String> GetCurrentDirFolder(string subFolder)
		{
			_curPath = Directory.GetCurrentDirectory ()+ subFolder;
			_curDirectoryFolderPaths = new List<String> ();

			foreach (var folderPath in Directory.GetDirectories( _curPath )) {
				try 
				{
					this._curDirectoryFolderPaths.Add(folderPath);
				} 
				catch (Exception ex) 
				{
					lastError = ex.Message;
				}
			}
			Debug.Log ("Found " + this._curDirectoryFolderPaths.Count.ToString () + " Folder(s) in this Directory ");

			return this._curDirectoryFolderPaths;
		}

		public List<MusicFromDeviceDataModel> GetAudioFilesInDir()
		{
			this.audioFilesInDir = new List<MusicFromDeviceDataModel>();
			this.DirSearch (_curPath);
			return this.audioFilesInDir;
		}
			
		private void DirSearch(string sDir) 
		{
			try	
			{
				foreach (string d in Directory.GetDirectories(sDir)) 
				{
					foreach (string f in Directory.GetFiles(d, "*.mp3")) 
					{
						var songName = f.Split('/')[f.Split('/').Length];
						var song = new MusicFromDeviceDataModel(){FullPath = f, SongName = songName};
						this.audioFilesInDir.Add(song);
					}
					DirSearch(d);
				}
			}
			catch (System.Exception excpt) 
			{
				Console.WriteLine(excpt.Message);
			}
		}
	}
}

