using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ExpandingItem : MonoBehaviour
{
  // how quickly the item should expands. Smaller is slower.
  public float scaleSpeedPerFrame;
  public Text NPCText;

  bool open;
  bool shouldUpdateExpand = false;
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

    open = true;
  }

  void Update()
  {
    if(NPCText.text == string.Empty && open)
    {
      DoShrink();
    }
  }

  IEnumerator Shrink()
  {
    while (rect.localScale.x > 0)
    {
      yield return null;
      rect.localScale -= new Vector3(scaleSpeedPerFrame, scaleSpeedPerFrame, scaleSpeedPerFrame);
    }

    open = false;
  }
}
