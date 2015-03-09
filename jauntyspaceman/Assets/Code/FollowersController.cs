using UnityEngine;

public class FollowersController : MonoBehaviour
{
  public GameObject MaleFollowerObject;
  public GameObject FemaleFollowerObject;

  public void EnableFollower(string followerName, Sprite newSprite)
  {
    if(newSprite.name.ToLower() == "male")
    {
      FemaleFollowerObject.SetActive(false);
      MaleFollowerObject.SetActive(true);
    }
    else
    {
      MaleFollowerObject.SetActive(false);
      FemaleFollowerObject.SetActive(true);
    }
  }

  public void DisableFollower()
  {
    MaleFollowerObject.SetActive(false);
    FemaleFollowerObject.SetActive(false);
  }
}
