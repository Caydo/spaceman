using UnityEngine;

public class FollowersController : MonoBehaviour
{
  public Transform[] FollowerTransforms;
  public float FollowerPaddingAmount;
  SpriteRenderer playerSpriteRenderer;
  float initialFollowerX;

  void Awake()
  {
    playerSpriteRenderer = GetComponent<SpriteRenderer>();
    //initialFollowerX = ((playerSpriteRenderer.sprite.textureRect.width + FollowerPaddingAmount) / 100) * -1;
  }

  void Start()
  {
    initFollowers();
  }

  void initFollowers()
  {
    for (int i = 0; i < FollowerTransforms.Length; i++)
    {
      Transform follower = FollowerTransforms[i];
      //FollowerTransforms[i].localPosition = new Vector3(initialFollowerX * i, follower.localPosition.y, follower.localPosition.z);
    }
  }

  void LateUpate()
  {
    for (int i = 0; i < FollowerTransforms.Length; i++)
    {
      Transform follower = FollowerTransforms[i];
      FollowerTransforms[i].localPosition = gameObject.transform.position * 3;
    }
  }
}
