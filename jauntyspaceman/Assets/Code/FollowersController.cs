using UnityEngine;

public class FollowersController : MonoBehaviour
{
  public GameObject FollowerObject;
  SpriteRenderer followerSprite;

  public void EnableFollower(string followerName)
  {
    FollowerObject.SetActive(true);
  }

  public void DisableFollower()
  {
    FollowerObject.SetActive(false);
  }
}
