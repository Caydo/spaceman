
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class SpriteTypes : MonoBehaviour {
	public Transform harmfulElement;
	public Transform terrainPiece; 
	public Transform powerUp;

	private void initDict() 
	{ 
		dict = new Dictionary<int, Transform>();
		dict.Add(2, terrainPiece); 
		dict.Add(29, terrainPiece); 
		dict.Add(16, powerUp);
	}
	IDictionary<int, Transform> dict = null; // new Dictionary<int, string>();

	public bool tryGetSpriteAsset(int type, out Transform result) 
	{ 
		if (dict == null)
		{
			initDict();
		}
		return dict.TryGetValue (type, out result);
	}

	void Start () { 
		initDict();
	}
}

