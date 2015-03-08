using UnityEngine;

public class FollowersController : MonoBehaviour
{
  public GameObject FollowerObject;
  public SpriteRenderer followerSprite;

  public void EnableFollower(string followerName, Sprite newSprite)
  {
    FollowerObject.SetActive(true);
    followerSprite.sprite = newSprite;
  }

  public void DisableFollower()
  {
    FollowerObject.SetActive(false);
  }
}
