using UnityEngine;

public class FollowersController : MonoBehaviour
{
  public GameObject MaleFollowerObject;
  public GameObject FemaleFollowerObject;

  public void EnableFollower(string followerName, Sprite newSprite)
  {
    if(newSprite.name.ToLower() == "male")
    {
      MaleFollowerObject.SetActive(true);
    }
    else
    {
      FemaleFollowerObject.SetActive(true);
    }
  }

  public void DisableFollower()
  {
    MaleFollowerObject.SetActive(false);
    FemaleFollowerObject.SetActive(false);
  }
}
