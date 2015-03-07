using UnityEngine;
using System.Collections;

public class SpriteLoader : MonoBehaviour {
	public int type = 0; 
	public string tileset = "";

	private const string texturePath = "Assets/Art/tilesets/";
	private const string textureSuffix = ".png"; 

	// Use this for initialization
	void Start () {
		string spriteAsset = "";
		if(SpriteTypes.tryGetSpriteAsset(type, out spriteAsset)) { 		
			Texture2D texture = (Texture2D) Resources.LoadAssetAtPath(texturePath + tileset + "/" + spriteAsset + textureSuffix, typeof(Texture2D));
			if(texture != null) { 
				gameObject.AddComponent<SpriteRenderer>();
				SpriteRenderer newSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
				Sprite newSprite = Sprite.Create(texture, new Rect(0f, 0f, 70f, 70f), new Vector2(0f, 0f), 128f);
				newSpriteRenderer.sprite = newSprite;
			}
		} else { 
			// TODO delete this object, type unknown
		}
	}
	
	// Update is called once per frame
	void Update () {
	}
}
