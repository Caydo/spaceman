using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class PlayerInteractionTrigger : MonoBehaviour
{
  public bool DestroyOnCollide;
  void OnCollisionEnter2D(Collision2D coll)
  {
    if(coll.gameObject.tag == "Player")
    {
      doAction();
      if(DestroyOnCollide)
      {
        GameObject.Destroy(gameObject);
      }
    }
  }

  protected virtual void doAction()
  {

  }
}
