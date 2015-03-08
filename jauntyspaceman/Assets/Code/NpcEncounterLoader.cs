using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Linq; 
using UnityEditor;

public class NpcEncounterLoader : MonoBehaviour {

	private const string modulePath = "Assets/NpcScripts/";
	private const string moduleSuffix = ".xml";
	private List<string> possibleLevels = new List<string>();

	public TextCrawl textPanel; 

	private IDictionary<string, string> responseTriggers = new Dictionary<string, string>();
	private XmlDocument currentTrigger; 

	void Start() { 
//		LoadNpcEncounter("Angel");
		LoadRandomNpcEncounter();
	}

	public void LoadRandomNpcEncounter() { 
		if(possibleLevels.Count <= 0) { 
			var paths = AssetDatabase.GetAllAssetPaths().Where(x => x.EndsWith("xml") && x.Contains(modulePath));
			
			foreach (var path in paths)
			{
				string randomPath = path.Replace(moduleSuffix, "").Replace(modulePath, ""); 
				
				Debug.Log ("Found item in path {" + path + "}{" + randomPath + "}");
				possibleLevels.Add (randomPath);
			}
		}
		LoadNpcEncounter(possibleLevels[Random.Range (0, possibleLevels.Count)]);
	}

	public void LoadNpcEncounter(string npcName) {
		Debug.Log ("loading npc chunk @ " + modulePath + npcName + moduleSuffix);
		TextAsset asset = (TextAsset) Resources.LoadAssetAtPath (modulePath + npcName + moduleSuffix, typeof(TextAsset));
		if ( asset != null) {
			currentTrigger = new XmlDocument(); 
			currentTrigger.LoadXml(asset.text);

			XmlNodeList points = currentTrigger.SelectNodes ("//point[@id=0]");
			foreach(XmlNode mainPoint in points) {
				string Text = mainPoint.Attributes.GetNamedItem("text").InnerText;
				XmlNodeList responses = mainPoint.SelectNodes("response");
				textPanel.TextToAdd += Text; 
				foreach(XmlNode response in responses) { 
					string responseKey = response.Attributes.GetNamedItem("key").InnerText;
					string responseText = response.Attributes.GetNamedItem ("text").InnerText;
					string trigger = response.Attributes.GetNamedItem ("trigger").InnerText;
					ParseTrigger(responseKey, trigger);
					textPanel.TextToAdd += "\n <fancy markup here for bold? >" + responseKey + "</markup>  " + responseText; 
				}
			}
		}
	}

	public void ParseTrigger(string responseKey, string triggerString) { 
		responseTriggers.Add(responseKey, triggerString); 
	}

	public void ProcessTrigger(string responseKey, string trigger) { 
		Debug.Log ("hit trigger {" + responseKey + "}{" + trigger + "}"); 
		responseTriggers.Clear();
	}

	public void Update() { 
		bool done = false;
		var iter = responseTriggers.GetEnumerator();
		while(!done && iter.MoveNext ()) {
			KeyValuePair<string, string> curPair = iter.Current;
			bool displayedToUser = textPanel.FullySetText;
			bool keyPressed = Input.GetKeyDown (curPair.Key.ToLower ());
			if(displayedToUser && keyPressed) { 
				done = true; 
				ProcessTrigger (curPair.Key, curPair.Value);
			}
		}
	}
}
