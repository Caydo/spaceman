using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public class LevelLoader : MonoBehaviour {
	public SpriteTypes spriteTypes; 
	public Transform playerRef; 
	public NpcEncounterLoader npcLoader; 

  private string[] tilesets = { 
    "tileset1"//,
    //"tileset2"
  };
  private string currentTileset = "";
  private IDictionary<int, int> levelsAtDifficulty;

//	public Transform spriteLoader;
	private int spritesMade = 0;
	private const int SPRITES_RIGHT_OFFSET = 20; // how many sprites we should show to the right of our character
	private const int VERTICAL_SIZE = 10; // number of vertical sprites 
	private const int ARBITRARY_LEVEL_DIFFICULTY_CUTOFF = 20; 

	private const string modulePath = "Modules/";
	private const string moduleSuffix = ".xml";

	private List<List<List<int>>> levelGrid = new List<List<List<int>>>(); // x, y index
	private int levelGridSize = 0; 

	private List<string> possibleLevels = new List<string>();
	public int levelDifficulty = 0; 
	public bool loadRandomLevels = true;
	public bool increaseDifficultyEachLevel = false;
	public string loadSpecificLevel = "";

	// Use this for initialization
	void Start () {
    levelsAtDifficulty = new Dictionary<int, int>(); 
    levelsAtDifficulty.Add (0, 1); 
    levelsAtDifficulty.Add (1, 3); 
    levelsAtDifficulty.Add (2, 3); 
    levelsAtDifficulty.Add (3, 3); 
	}

	private Regex levelNameRegex = new Regex("^(?<difficulty>\\d+)_");
	void LoadRandomLevel() {
    if (possibleLevels.Count <= 0)
    {
      var paths = Resources.LoadAll(modulePath);

      foreach (var path in paths)
      {
        string randomPath = path.name.Replace(moduleSuffix, "").Replace(modulePath, "");

        Debug.Log("Found item in path {" + path + "}{" + randomPath + "}");
        possibleLevels.Add(randomPath);
      }

      var levelsByDifficulty = possibleLevels.GroupBy (levelName => { 
        var m= levelNameRegex.Match (levelName); 
        if(m.Success) { 
          return int.Parse (m.Groups["difficulty"].Value);
        } else { 
          return ARBITRARY_LEVEL_DIFFICULTY_CUTOFF;
        }
      });
      Debug.LogFormat ("grouped size {0}", levelsByDifficulty.Count ());
      //possibleLevels.Clear ();
      List<string> newList = new List<string>(); 
      foreach(var levelGroup in levelsByDifficulty) { 
        Debug.LogFormat ("BANANAS");
        int difficulty = levelGroup.Key;
        int levelsPerDifficulty = 0; 
        levelsAtDifficulty.TryGetValue(difficulty, out levelsPerDifficulty);
        foreach(string level in levelGroup.Take (levelsPerDifficulty)) { 
          Debug.LogFormat ("Load level {0} @ difficulty {1}", level, difficulty);
          newList.Add (level);
        }
      }
      possibleLevels = newList;
    }

    if (loadSpecificLevel != "")
    {
      Debug.Log("loadspecific level!!");
      Debug.Log(loadSpecificLevel);
      LoadChunk(loadSpecificLevel);
    }
    else if (loadRandomLevels)
    {
			List<string> levels; 
			do { 
				var things = possibleLevels.Where (levelName => { 
					Match m = levelNameRegex.Match(levelName);
					if (m.Success) { 
						int difficulty = int.Parse (m.Groups["difficulty"].Value);
						return difficulty == levelDifficulty;
					} else { 
						return false;
					}
				});
				levels = new List<string>(things);
			} while ((levels.Count <= 0) && (++levelDifficulty < ARBITRARY_LEVEL_DIFFICULTY_CUTOFF));

      if(levels.Count <= 0) { 
      // we ran out of levels even after trying higher difficulties; reset to 1!
        levelDifficulty = 1;
        //LoadRandomLevel ();
      } else { 
  			if(increaseDifficultyEachLevel) { 
  				levelDifficulty++;
  			}
  			int selectedLevel = Random.Range (0, levels.Count);
  			possibleLevels.Remove(levels[selectedLevel]);
  			LoadChunk(levels[selectedLevel]);
      }
    }
    else
    {
      LoadChunk("Teeth");
    }
	}

	void LoadChunk(string filename) { 
    Debug.LogFormat ("loading chunk {0} at difficulty {1}", modulePath + filename, levelDifficulty);
    TextAsset asset = Resources.Load(modulePath + filename) as TextAsset;
    Debug.Log((asset == null));
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

    currentTileset = tilesets[Random.Range (0, tilesets.Count())];
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
              string spriteName = "";
							if(spriteTypes.tryGetSpriteAsset(thisTileType, out spriteLoader, out spriteName)) { 
								Transform newItem = (Transform) Instantiate(
									spriteLoader, 
									gameObject.transform.position,
									Quaternion.identity
								);

								if(thisTileType == 27) { 
									newItem.gameObject.tag = "Respawn";
								}

                if(spriteName != "") { 
                  SpriteRenderer sr = newItem.gameObject.GetComponent<SpriteRenderer>(); 
                  if (sr != null) {                     
                    Texture2D texture = Resources.Load ("tilesets/" + currentTileset + "/" + spriteName.Replace(".png", "")) as Texture2D;
                    Sprite newSprite = Sprite.Create (
                      texture, 
                      new Rect(0f, 0f, 70f, 70f),  // use whole sprite
                      new Vector2(.5f, .5f),   // pivot = center
                      70f);                    // 70 pixels per unity unit 
                    Debug.Log ("Assigning sprite " + newSprite + " :: " + texture + " :: " + spriteName);
                    newSprite.name = spriteName.Replace (".png", "");
                    sr.sprite = newSprite;                   
                  }
                }

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
