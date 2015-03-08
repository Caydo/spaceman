using UnityEngine;

class GameOverOnInvis : MonoBehaviour
{
  public PlayerController Player;

  void OnBecameInvisible()
  {
    if (gameObject.activeInHierarchy)
    {
      StartCoroutine(Player.WaitThenRespawn());
    }
  }
}
