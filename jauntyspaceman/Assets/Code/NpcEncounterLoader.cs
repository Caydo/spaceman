using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Linq; 
using System.Text.RegularExpressions;
using System.Collections;

public class NpcEncounterLoader : MonoBehaviour {

	private const string modulePath = "NPCs/";
	private const string moduleSuffix = ".xml";
	private List<string> possibleLevels = new List<string>();

	public TextCrawl textPanel; 
	public OxygenBarController o2Controller; 

	private IDictionary<string, string> responseTriggers = new Dictionary<string, string>();
	private XmlDocument currentTrigger; 

	void Start() { 
		LoadRandomNpcEncounter();
	}

	public void LoadRandomNpcEncounter() {
    if (possibleLevels.Count <= 0)
    {
      var paths = Resources.LoadAll(modulePath);

      foreach (var path in paths)
      {
        string randomPath = path.name.Replace(moduleSuffix, "").Replace(modulePath, "");

        Debug.Log("Found item in path {" + path + "}{" + randomPath + "}");
        possibleLevels.Add(randomPath);
      }
    }
    LoadNpcEncounter(possibleLevels[Random.Range(0, possibleLevels.Count)]);
	}

	public void LoadNpcEncounter(string npcName) {
		Debug.Log ("loading npc chunk @ " + "NPCs/" + npcName);
    TextAsset asset = Resources.Load("NPCs/" + npcName) as TextAsset;
		if ( asset != null) {
			currentTrigger = new XmlDocument(); 
			currentTrigger.LoadXml(asset.text);

			XmlNode point = currentTrigger.SelectSingleNode ("//point[@id=0]");
			parsePoint (point);
		}
	}
	
	IEnumerator TriggerPointCoroutine(string coroutineTrigger) { 
		Debug.Log ("trigger point coroutine entered");
		while(!textPanel.FullySetText) { 
			yield return new WaitForSeconds(1);
		}
		Debug.Log ("trigger point coroutine finishing");
		ProcessTrigger (null, coroutineTrigger);
	}

	public void parsePoint(XmlNode mainPoint) { 
		Debug.Log ("parsePoint entereted {" + mainPoint.Attributes + "}");
		textPanel.Reset();
		string text = mainPoint.Attributes.GetNamedItem("text").InnerText;
		textPanel.TextToAdd += text; 

		XmlNode trigger = mainPoint.Attributes.GetNamedItem("trigger"); 
		if(trigger != null) { 
			StartCoroutine(TriggerPointCoroutine(trigger.InnerText));
		} else { 
			Debug.Log ("Looking for responses");
			XmlNodeList responses = mainPoint.SelectNodes("response");
			foreach(XmlNode response in responses) { 
				string responseKey = response.Attributes.GetNamedItem("key").InnerText;
				string responseText = response.Attributes.GetNamedItem ("text").InnerText;
				string triggerText = response.Attributes.GetNamedItem ("trigger").InnerText;
				ParseTrigger(responseKey, triggerText);
				textPanel.TextToAdd += "\n <fancy markup here for bold? >" + responseKey + "</markup>  " + responseText; 
			}
		}
		textPanel.StartCrawl();
	}
	
	public void ParseTrigger(string responseKey, string triggerString) { 
		responseTriggers.Add(responseKey, triggerString); 
	}

	private Regex gotoRegex = new Regex("Go To (?<point>\\w+)");
	private Regex giveO2Regex = new Regex("Grant (?<amt>\\d+) O2");
	private Regex takeO2Regex = new Regex("Take (?<amt>\\d+) O2");
	private Regex endRegex = new Regex("EndDialog");

	public void ProcessTrigger(string responseKey, string trigger) { 
		Debug.Log ("hit trigger {" + responseKey + "}{" + trigger + "}"); 
		responseTriggers.Clear();

		foreach(string triggerPart in Regex.Split(trigger, "\\s+,\\s+")) {
			Debug.Log("parse trigger part {" + triggerPart + "}");
			Match match; 
			match = gotoRegex.Match(triggerPart);
			if(match.Success) { 
				string pointId = match.Groups["point"].Value;
				XmlNode thePoint = currentTrigger.SelectSingleNode("//point[@id=" + pointId + "]");
				if(thePoint != null) { 
					parsePoint (thePoint);
				} else { 
					Debug.LogWarning("reference to missing point [" + pointId + "]");
				}
			}

			match = giveO2Regex.Match (triggerPart);
			if(match.Success) { 
				Debug.Log ("amt" + match.Groups["amt"].Value);
				int amt = int.Parse (match.Groups["amt"].Value);
				o2Controller.GainOxygen(amt);
			}

			match = takeO2Regex.Match (triggerPart); 
			if(match.Success) { 
				int amt = int.Parse (match.Groups["amt"].Value);
				o2Controller.LoseOxygen(amt);
			}

			match = endRegex.Match (triggerPart); 
			if(match.Success) { 
				textPanel.Reset();
			}
		}
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
