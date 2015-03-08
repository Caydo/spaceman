using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;
using System.Collections.Generic;
using UnityEditor;
using System.Linq;

public class LevelLoader : MonoBehaviour {
	public SpriteTypes spriteTypes; 

//	public Transform spriteLoader;
	private int spritesMade = 0;
	private const int SPRITES_RIGHT_OFFSET = 10; // how many sprites we should show to the right of our character
	private const int VERTICAL_SIZE = 10; // number of vertical sprites 

	private const string modulePath = "Assets/Modules/";
	private const string moduleSuffix = ".xml";

	private List<List<int>> levelGrid = new List<List<int>>(); // x, y index
	private int levelGridSize = 0; 

	private List<string> possibleLevels = new List<string>();
	public bool loadRandomLevels = true;

	// Use this for initialization
	void Start () {
	}

	void LoadRandomLevel() { 
		if(possibleLevels.Count <= 0) { 
			var paths = AssetDatabase.GetAllAssetPaths().Where(x => x.EndsWith("xml"));
			
			foreach (var path in paths)
			{
				string randomPath = path.Replace(".xml", "").Replace("Assets/Modules/", ""); 

				Debug.Log ("Found item in path {" + path + "}{" + randomPath + "}");
				possibleLevels.Add (randomPath);
			}
		}

		if(loadRandomLevels) { 
			LoadChunk(possibleLevels[Random.Range (0, possibleLevels.Count)]);
		} else { 
			LoadChunk("Teeth");
		}
	}

	void LoadChunk(string filename) { 
		Debug.Log ("loading chunk @ " + modulePath + filename + moduleSuffix);
		TextAsset asset = (TextAsset) Resources.LoadAssetAtPath (modulePath + filename + moduleSuffix, typeof(TextAsset));
		if ( asset != null) {
			XmlDocument xmlDoc = new XmlDocument(); 
			xmlDoc.LoadXml(asset.text);

			int width = 0; 
			int height = 0; 
			XmlNodeList layers = xmlDoc.SelectNodes ("//layer");
			foreach(XmlNode layer in layers) { 
				width = int.Parse(layer.Attributes.GetNamedItem("width").Value);
				height = int.Parse(layer.Attributes.GetNamedItem ("height").Value);

				XmlNode data = layer.SelectSingleNode("data");
				IEnumerator tiles = data.SelectNodes("tile").GetEnumerator();
				List<List<int>> yLists = new List<List<int>>(); 
				for(int y = height; y > 0; y--) {
					List<int> tilesThisYLayer = new List<int>();
					for(int x = 0; x < width; ++x) { 
						tiles.MoveNext();
						XmlNode tile = (XmlNode) tiles.Current;
						tilesThisYLayer.Add(int.Parse(tile.Attributes.GetNamedItem("gid").Value));
					}
					yLists.Add(tilesThisYLayer);
				}

				for(int x = 0; x < width; x++) { 
					List<int> nextList = new List<int>();
					foreach(List<int> yList in yLists) { 
						nextList.Add (yList[x]);
					}
					levelGrid.Add(nextList);
				}

				levelGridSize += width;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		int desiredSprites = Mathf.FloorToInt(Mathf.Abs(gameObject.transform.position.x));
		if(SPRITES_RIGHT_OFFSET + desiredSprites > spritesMade){
			int numSprites = SPRITES_RIGHT_OFFSET + desiredSprites - spritesMade;
			if(numSprites > 1000) { 
				throw new UnityException("bad things!");
			}
			float baseX = spritesMade;
			spritesMade += numSprites;
			for (int x = 0; x < numSprites; ++x) { 
				for (int y = 0; y < VERTICAL_SIZE; ++y) { 
					if(baseX + x >= levelGridSize) { 
						LoadRandomLevel();
					}
					int thisTileType = levelGrid[(int)baseX + x][y];
					if(thisTileType != 0) { 					
						Transform spriteLoader = null; 
						if(spriteTypes.tryGetSpriteAsset(thisTileType, out spriteLoader)) { 
							Transform newItem = (Transform) Instantiate(
								spriteLoader, 
								gameObject.transform.position,
								Quaternion.identity
							);
							newItem.transform.parent = gameObject.transform;
							newItem.Translate(new Vector3(baseX + x , VERTICAL_SIZE - y, 0));
						}
					}
				}
			}
		}
	}
}
