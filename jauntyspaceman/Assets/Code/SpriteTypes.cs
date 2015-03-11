
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class SpriteTypes : MonoBehaviour {
	public Transform harmfulElement;
	public Transform powerUp;

	public Transform basicBlock; 
	public Transform ceilingDown1; 
	public Transform ceilingUp; 
	public Transform slopeDown;
	public Transform slopeUp; 
	public Transform spikes; 
	public Transform water; 
	public Transform waterBlock; 
	public Transform invisBlock; 
	public Transform respawnPoint;
	public Transform bridge;
	public Transform cat;
	public Transform npc; 
	public Transform princess; 

	private void initDict() 
	{ 
		new KeyValuePair<Transform, string>(basicBlock, "foo");
		// TODO we were gonna try to make these strings referencing the art assets instead of ids?
		dict = new Dictionary<int, KeyValuePair<Transform, string>>();
		dict.Add (2 , new KeyValuePair<Transform, string>(basicBlock, "ground_dirt")); 
    dict.Add (8 , new KeyValuePair<Transform, string>(ceilingDown1, "CeilingDown")); 
    dict.Add (9 , new KeyValuePair<Transform, string>(ceilingUp, "CeilingUp")); 
    dict.Add (27, new KeyValuePair<Transform, string>(respawnPoint, "")); 
    dict.Add (28, new KeyValuePair<Transform, string>(slopeDown, "SlopeDown")); 
    dict.Add (29, new KeyValuePair<Transform, string>(slopeUp, "SlopeUp")); 
    dict.Add (30, new KeyValuePair<Transform, string>(spikes, "spikes")); 
    dict.Add (31, new KeyValuePair<Transform, string>(water, "water")); 
    dict.Add (32, new KeyValuePair<Transform, string>(waterBlock, "water_block")); 
    dict.Add (20, new KeyValuePair<Transform, string>(basicBlock, "ground_dirt")); 
    dict.Add (21, new KeyValuePair<Transform, string>(invisBlock, "")); 
    dict.Add (5 , new KeyValuePair<Transform, string>(bridge, "bridge")); 
    dict.Add (6 , new KeyValuePair<Transform, string>(powerUp, "")); 
    dict.Add (3 , new KeyValuePair<Transform, string>(cat, "")); 
    dict.Add (24, new KeyValuePair<Transform, string>(npc, "")); 
    dict.Add (4 , new KeyValuePair<Transform, string>(princess, "")); 
//		dict.Add (8, ceilingDown1);
//		dict.Add (9, ceilingUp);
//		dict.Add (27, respawnPoint);
//		dict.Add (28, slopeDown); 
//		dict.Add (29, slopeUp); 
//		dict.Add (30, spikes); 
//		dict.Add (31, water); 
//		dict.Add (32, waterBlock);
//		dict.Add (20, basicBlock);
//		dict.Add (21, invisBlock);
//		dict.Add (5, bridge);
//		dict.Add (6, powerUp);
//		dict.Add (3, cat);
//		dict.Add (24, npc);
//		dict.Add (4, princess);
	}
	IDictionary<int, KeyValuePair<Transform, string>> dict = null; // new Dictionary<int, string>();

	public bool tryGetSpriteAsset(int type, out Transform result, out string spriteName) 
	{ 
		if (dict == null)
		{
			initDict();
		}
    KeyValuePair<Transform, string> kvp; 
    bool resultA = dict.TryGetValue (type, out kvp);
    if(resultA) { 
      result = kvp.Key;
      spriteName = kvp.Value;
    } else { 
      result = null;
      spriteName = "";
    }
    return resultA;
	}

	void Start () { 
		initDict();
	}
}

