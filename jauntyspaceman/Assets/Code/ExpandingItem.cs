using System.Collections;
using UnityEngine;

public class ExpandingItem : MonoBehaviour
{
  // how quickly the item should expands. Smaller is slower.
  public float scaleSpeedPerFrame;
  RectTransform rect;
  bool doneScaling;

  void Awake()
  {
    rect = GetComponent<RectTransform>();
  }

  public void DoExpand()
  {
    StartCoroutine(Expand());
  }

  public void DoShrink()
  {
    StartCoroutine(Shrink());
  }

  IEnumerator Expand()
  {
    while(rect.localScale.x < 1)
    {
      rect.localScale += new Vector3(scaleSpeedPerFrame, scaleSpeedPerFrame, scaleSpeedPerFrame);
      yield return null;
    }
  }

  IEnumerator Shrink()
  {
    while (rect.localScale.x > 0)
    {
      yield return null;
      rect.localScale -= new Vector3(scaleSpeedPerFrame, scaleSpeedPerFrame, scaleSpeedPerFrame);
    }
  }
}
