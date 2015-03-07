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

  void Update()
  {
    scaleAmount = scaleSpeedPerFrame * Time.deltaTime;
    doneScaling = (rect.localScale.x >= 1);

    if(!doneScaling)
    {
      rect.localScale += new Vector3(scaleAmount, scaleAmount, scaleAmount);
    }
  }
}
