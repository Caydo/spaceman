using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;

public class NpcEncounterLoader : MonoBehaviour {

	private const string modulePath = "Assets/NpcScripts/";
	private const string moduleSuffix = ".xml";

	public TextCrawl textPanel; 

	private XmlDocument currentTrigger; 

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
				foreach(XmlNode response in responses) { 
					string responseKey = response.Attributes.GetNamedItem("id").InnerText;
					string responseText = response.Attributes.GetNamedItem ("text").InnerText;
					string trigger = response.Attributes.GetNamedItem ("trigger").InnerText;
					ParseTrigger(trigger);
				}
			}
		}
	}

	public void ParseTrigger(string triggerString) { 
	}
}
