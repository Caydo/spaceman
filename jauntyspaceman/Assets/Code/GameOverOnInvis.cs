using UnityEngine;

class GameOverOnInvis : MonoBehaviour
{
  public PlayerController Player = null;

  void OnBecameInvisible()
  {
    if(gameObject.activeInHierarchy)
    {
      if(Player.ShouldRespawn)
      {
        StartCoroutine(Player.WaitThenRespawn());
      }
    }
  }
}
