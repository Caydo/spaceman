using UnityEngine;

class GameOverOnInvis : MonoBehaviour
{
  public PlayerController Player;

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
