using UnityEngine;

public class ClickableItem : MonoBehaviour
{
  void OnMouseUp()
  {
    doAction();
  }

  protected virtual void doAction()
  {
  }
}
