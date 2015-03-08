using UnityEngine;

class GameOverOnInvis : MonoBehaviour
{
  void OnBecameInvisible()
  {
    Application.LoadLevel("GameOver");
  }
}
