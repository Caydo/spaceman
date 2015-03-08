using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

public class FindScriptReferences : MonoBehaviour
{
	const string NAME = "Tools/Reload Modules";
	
	[MenuItem(NAME, true)]
	static bool validate()
	{
		return Selection.activeObject != null && Selection.activeObject.GetType() == typeof(MonoScript);
	}
	
	/// <summary>
	/// Search all project prefabs for a specified mono component.
	/// </summary>
	[MenuItem(NAME)]
	static void run()
	{
		var paths = AssetDatabase.GetAllAssetPaths().Where(x => x.EndsWith("tmx"));
		
		foreach (var path in paths)
		{
			Debug.Log ("Found item in path {" + path + "}");
			string newPath = path.Replace ("tmx", "xml");
			if(System.IO.File.Exists(newPath)) {
				try { 
					System.IO.File.Delete (newPath);
				} catch (Exception e) { 
					Debug.Log ("info: couldn't delete existing xml file! " + e.ToString ());
				}
			}
			try { 
				System.IO.File.Copy (path, newPath);
			} catch (Exception e) { 
				Debug.Log ("info: xml file already exists " + e.ToString());
			}

		}
	}
}
