
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

	private void initDict() 
	{ 
		dict = new Dictionary<int, Transform>();
		dict.Add (2, basicBlock); 
		dict.Add (8, ceilingDown1);
		dict.Add (9, ceilingUp);
		dict.Add (28, slopeDown); 
		dict.Add (29, slopeUp); 
		dict.Add (30, spikes); 
		dict.Add (31, water); 
		dict.Add (32, waterBlock);
		dict.Add (20, basicBlock);
		dict.Add (6, powerUp);
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

