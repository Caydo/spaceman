using UnityEngine;

public class Clicky : MonoBehaviour
{
  void OnMouseUp()
  {
    doAction();
  }

  protected virtual void doAction()
  {
  }
}
