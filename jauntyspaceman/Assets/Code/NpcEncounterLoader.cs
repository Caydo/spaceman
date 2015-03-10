using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Linq; 
using System.Text.RegularExpressions;
using System.Collections;
using UnityEngine.UI;

public class NpcEncounterLoader : PlayerInteractionTrigger {

	private const string modulePath = "NPCs/";
	private const string moduleSuffix = ".xml";
	private List<string> possibleLevels = new List<string>();
	

  public Sprite FollowerSprite;
	public TextCrawl textPanel; 
	public OxygenBarController o2Controller;
  ExpandingItem textPanelExpander;

	private IDictionary<string, string> responseTriggers = new Dictionary<string, string>();
	private XmlDocument currentTrigger; 

	void Start() { 
		textPanel = GameObject.FindWithTag("TextCrawl").GetComponent<TextCrawl>();
		o2Controller = GameObject.FindWithTag ("OxygenController").GetComponent<OxygenBarController>();
		textPanelExpander = GameObject.FindWithTag("TextPanel").GetComponent<ExpandingItem>();
		LoadRandomNpcEncounter ();
	}

	private XmlNode pendingPoint = null; 
	protected override void doTriggeredAction()
	{	
		foreach(GameObject go in GameObject.FindGameObjectsWithTag ("NPCTag")) { 
			if(go.GetInstanceID() != gameObject.GetInstanceID()) { 
				var nel = go.GetComponent<NpcEncounterLoader>();
				if( nel != null ) { 
					nel.Fail ();
				}
			}
		}

		if(pendingPoint != null) { 
			var thePoint = pendingPoint; 
			pendingPoint = null;
			parsePoint (thePoint);
		}
	}

	public void Fail() { 
		// TODO do bad thing?
		Debug.Log ("Hit fail case for npc encounter loader");
		responseTriggers.Clear();
		o2Controller.LoseDefaultO2();
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
		int selectedLevel = Random.Range(0, possibleLevels.Count);
		string level = possibleLevels[selectedLevel]; 
		possibleLevels.Remove (level);
		LoadNpcEncounter(level);
		//LoadNpcEncounter ("Fenix");
	}

	public void LoadNpcEncounter(string npcName) {
		Debug.Log ("loading npc chunk @ " + "NPCs/" + npcName);
 	    TextAsset asset = Resources.Load("NPCs/" + npcName) as TextAsset;
		if ( asset != null) {
			currentTrigger = new XmlDocument(); 
			currentTrigger.LoadXml(asset.text);

			SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>(); 
			if (sr != null) { 

				XmlNode spriteFile = currentTrigger.SelectSingleNode("//spriteset");
				string spriteFileName = spriteFile.Attributes.GetNamedItem("file").Value;
				Texture2D texture = Resources.Load ("tilesets/npcs/" + spriteFileName.Replace(".png", "")) as Texture2D;
				Sprite newSprite = Sprite.Create (
					texture, 
					new Rect(0f, 0f, 70f, 70f),  // use whole sprite
					new Vector2(.5f, .5f),   // pivot = center
					70f);                    // 70 pixels per unity unit 
				Debug.Log ("Assigning sprite " + newSprite + " :: " + texture + " :: " + spriteFileName);
				newSprite.name = spriteFileName.Replace (".png", "");
				sr.sprite = newSprite;

        FollowerSprite = newSprite;
			}

			XmlNode point = currentTrigger.SelectSingleNode ("//point[@id=0]");
			pendingPoint = point;

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

	private Regex colorRegex = new Regex("#(?<r>\\w\\w)(?<g>\\w\\w)(?<b>\\w\\w)(?<a>\\w\\w)");
	public void parsePoint(XmlNode mainPoint) { 
		Debug.Log ("parsePoint entereted {" + mainPoint.Attributes + "}");

		textPanel.Reset();

		string name = currentTrigger.SelectSingleNode("//npc").Attributes.GetNamedItem ("name").Value;
		textPanel.TextToAdd += name + " says:\n";

		string text = mainPoint.Attributes.GetNamedItem("text").InnerText;
		textPanel.TextToAdd += text; 



		Debug.Log ("Looking for responses");
		XmlNodeList responses = mainPoint.SelectNodes("response");
		foreach(XmlNode response in responses) { 
			string responseKey = response.Attributes.GetNamedItem("key").InnerText;
			if(responseKey.Equals("(timeout)")) { 
				// TODO set up the bad thing that happens if you don't handle it 
			} else { 
				string responseText = response.Attributes.GetNamedItem ("text").InnerText;
				string triggerText = response.Attributes.GetNamedItem ("trigger").InnerText;
				ParseTrigger(responseKey, triggerText);
				textPanel.AdditionalText += "\n <b>" + responseKey + ":</b>  " + responseText; 
			}
		}

		XmlNode trigger = mainPoint.Attributes.GetNamedItem("trigger"); 
		if(trigger != null) { 
			if(responses.Count > 0) { 
				ProcessTrigger (null, trigger.InnerText);
			} else { 
				StartCoroutine(TriggerPointCoroutine(trigger.InnerText));
			}
		} 

		XmlNode colorNode = mainPoint.Attributes.GetNamedItem ("color");
		if(colorNode != null && colorNode.InnerText != null) { 
			Match colorMatch = colorRegex.Match (colorNode.InnerText);
			if(colorMatch.Success) { 
				Text tp = textPanel.textToCrawl.GetComponent<Text>();
				tp.color = new Color(
					stringToFloat(colorMatch.Groups["r"].Value),
					stringToFloat(colorMatch.Groups["g"].Value),
					stringToFloat(colorMatch.Groups["b"].Value),
					stringToFloat(colorMatch.Groups["a"].Value)
				);
			}
		}

		textPanel.StartCrawl();
    	textPanelExpander.DoExpand();
	}

	public float stringToFloat(string input) { 
		int value = int.Parse(input, System.Globalization.NumberStyles.HexNumber);
		return ((float) value) / 15;
	}

	public void ParseTrigger(string responseKey, string triggerString) { 
		responseTriggers.Add(responseKey, triggerString); 
	}

	private Regex gotoRegex = new Regex("Go To (?<point>\\w+)");
	private Regex giveO2Regex = new Regex("Gain (?<amt>[\\.\\d]+) O2");
	private Regex takeO2Regex = new Regex("Take (?<amt>[\\.\\d]+) O2");
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
				Debug.Log("point Id serach {" + "//point[@id=" + pointId + "]}");
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
				float amt = float.Parse (match.Groups["amt"].Value);
				o2Controller.GainOxygen(amt);
			}

			match = takeO2Regex.Match (triggerPart); 
			if(match.Success) { 
				float amt = float.Parse (match.Groups["amt"].Value);
				o2Controller.LoseOxygen(amt);
			}

			match = endRegex.Match (triggerPart); 
			if(match.Success) { 
				textPanel.Reset();
        textPanelExpander.DoShrink();
        GameObject.FindWithTag("Player").GetComponent<FollowersController>().DisableFollower();
				GameObject.Destroy(gameObject);
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
