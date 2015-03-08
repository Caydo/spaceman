using System.Collections;
using UnityEngine;

public class ExpandingItem : MonoBehaviour
{
  // how quickly the item should expands. Smaller is slower.
  public float scaleSpeedPerFrame;
  RectTransform rect;
  float scaleAmount;
  bool doneScaling;

  void Awake()
  {
    rect = GetComponent<RectTransform>();
  }

  IEnumerator Expand()
  {
    while(rect.localScale.x <= 0)
    {
      yield return null;
      rect.localScale += new Vector3(scaleAmount, scaleAmount, scaleAmount);
    }
  }

  IEnumerator Shrink()
  {
    while (rect.localScale.x > 0)
    {
      yield return null;
      rect.localScale -= new Vector3(scaleAmount, scaleAmount, scaleAmount);
    }
  }
}
