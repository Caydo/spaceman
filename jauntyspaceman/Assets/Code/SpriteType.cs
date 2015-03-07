
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class SpriteTypes {
	private static void initDict() 
	{ 
		dict = new Dictionary<int, string>();
		dict.Add(1, "ground_1");
		dict.Add(2, "spike_1");
	}
	static IDictionary<int, string> dict = null; // new Dictionary<int, string>();

	public static bool tryGetSpriteAsset(int type, out string result) 
	{ 
		if (dict == null)
		{
			initDict();
		}
		return dict.TryGetValue (type, out result);
	}
}

