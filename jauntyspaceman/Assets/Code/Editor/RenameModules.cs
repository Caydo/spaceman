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
			if(!System.IO.File.Exists(newPath)) { 
				try { 
					System.IO.File.Copy (path, newPath);
				} catch (Exception e) { 
					Debug.Log ("info: xml file already exists");
				}
			}
//				var assets = AssetDatabase.LoadAllAssetsAtPath(path);
//				
//				foreach (var asset in assets)
//				{
//					GameObject gObj = asset as GameObject;
//					if (gObj != null)
//					{
//						var monos = gObj.GetComponents<MonoBehaviour>().Where(x => x != null);
//						foreach (var m in monos)
//						{
//							if (m.GetType().IsAssignableFrom(monoType))
//							{
//								gObjs.Add(asset as GameObject);
//							}
//						}
//					}
//				}
		}
		string output = string.Format("Search complete, found {0} object(s) with the {1} component.");
		Debug.Log(output);
	}
}