using UnityEngine;

public class DestroyOnInvisible : MonoBehaviour
{
  void OnBecameInvisible()
  {
    GameObject.Destroy(gameObject);
  }
}