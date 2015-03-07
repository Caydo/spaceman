using UnityEngine;

public class ClickableItem : MonoBehaviour
{
  void OnMouseUp()
  {
    doAction();
  }

  protected virtual void doAction()
  {
    // do a thing on click
  }
}
