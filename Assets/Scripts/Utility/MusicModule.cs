using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace AssemblyCSharp
{
	public class MusicModule
	{
		private string _curPath  ;
		private List<String> _curDirectoryFolderPaths = new List<String>() ;

		public void GetCurDirFolders ()
		{
			_curPath = Directory.GetCurrentDirectory ();
			foreach (var folderPath in Directory.GetDirectories( _curPath )) {
				try {
					_curDirectoryFolderPaths.Add (folderPath);

				} catch (Exception ex) {
					Debug.Log (ex.Message);
				}
			}
			Debug.Log ("Found " + _curDirectoryFolderPaths.Count.ToString () + " Folder(s) in this Directory ");
		}
	}
}

