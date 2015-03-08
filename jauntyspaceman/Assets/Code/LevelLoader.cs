using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;
using System.Collections.Generic;
using UnityEditor;
using System.Linq;

public class LevelLoader : MonoBehaviour {
	public SpriteTypes spriteTypes; 
	public Transform playerRef; 

//	public Transform spriteLoader;
	private int spritesMade = 0;
	private const int SPRITES_RIGHT_OFFSET = 10; // how many sprites we should show to the right of our character
	private const int VERTICAL_SIZE = 10; // number of vertical sprites 

	private const string modulePath = "Assets/Modules/";
	private const string moduleSuffix = ".xml";

	private List<List<List<int>>> levelGrid = new List<List<List<int>>>(); // x, y index
	private int levelGridSize = 0; 

	private List<string> possibleLevels = new List<string>();
	public bool loadRandomLevels = true;
	public string loadSpecificLevel = "";

	// Use this for initialization
	void Start () {
	}

	void LoadRandomLevel() { 
		if(possibleLevels.Count <= 0) { 
			var paths = AssetDatabase.GetAllAssetPaths().Where(x => x.EndsWith("xml") && x.Contains(modulePath));
			
			foreach (var path in paths)
			{
				string randomPath = path.Replace(moduleSuffix, "").Replace(modulePath, ""); 

				Debug.Log ("Found item in path {" + path + "}{" + randomPath + "}");
				possibleLevels.Add (randomPath);
			}
		}

		if(loadSpecificLevel != "") { 
			LoadChunk (loadSpecificLevel);
		} else if(loadRandomLevels) { 
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
			List<List<List<int>>> yLists = new List<List<List<int>>>(); 
			foreach(XmlNode layer in layers) { 
				width = int.Parse(layer.Attributes.GetNamedItem("width").Value);
				height = int.Parse(layer.Attributes.GetNamedItem ("height").Value);

				XmlNode data = layer.SelectSingleNode("data");
				IEnumerator tiles = data.SelectNodes("tile").GetEnumerator();

				for(int y = height - 1; y >= 0; y--) {
					List<List<int>> tilesThisYLayer;
					Debug.Log ("y list size {" + yLists.Count + "}{" + y + "}");
					if(yLists.Count < y) { 
						for(int i = 0; i <= y; ++i) { 
							yLists.Add (new List<List<int>>());
						}
					} 

					tilesThisYLayer = yLists[y];				
					 
					for(int x = 0; x < width; ++x) { 
						List<int> miniXList;
						if(tilesThisYLayer.Count <= x) {
							for(int i = 0; i <= x; ++i) { 
								tilesThisYLayer.Add (new List<int>());
							}
						}
						miniXList = tilesThisYLayer[x];
						tiles.MoveNext();
						XmlNode tile = (XmlNode) tiles.Current;
						miniXList.Add(int.Parse(tile.Attributes.GetNamedItem("gid").Value));
					}
					yLists.Add(tilesThisYLayer);
				}			
			}

			for(int x = 0; x < width; x++) { 
				List<List<int>> nextList = new List<List<int>>();
				foreach(List<List<int>> yList in yLists) { 
					nextList.Add (yList[x]);
				}
				levelGrid.Add(nextList);
			}
			
			levelGridSize += width;
		}
	}
	
	// Update is called once per frame
	void Update () {
		int desiredSprites = Mathf.FloorToInt(Mathf.Abs(playerRef.transform.position.x));
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
					List<int> thisTileTypes = levelGrid[(int)baseX + x][y];
					foreach(int thisTileType in thisTileTypes) { 
						if(thisTileType != 0) { 					
							Transform spriteLoader = null; 
							if(spriteTypes.tryGetSpriteAsset(thisTileType, out spriteLoader)) { 
								Transform newItem = (Transform) Instantiate(
									spriteLoader, 
									gameObject.transform.position,
									Quaternion.identity
								);
								newItem.transform.parent = gameObject.transform;
								newItem.Translate(new Vector3(baseX + x , y, 0));
							}
						}
					}
				}
			}
		}
	}
}
