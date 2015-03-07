using UnityEngine;

public class ScrollingItem : MonoBehaviour
{
  // how quickly the item should scroll. Smaller is slower.
  public float scrollSpeedPerFrame;

  void Update()
  {
    gameObject.transform.Translate(new Vector3(0.01f, 0, 0));
  }
}