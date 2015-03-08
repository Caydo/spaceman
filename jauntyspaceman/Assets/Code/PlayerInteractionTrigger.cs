using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class PlayerInteractionTrigger : MonoBehaviour
{
  public bool DestroyOnCollide;
  public bool DestroyOnTrigger;
  void OnCollisionEnter2D(Collision2D coll)
  {
    if(coll.gameObject.tag == "Player")
    {
      doCollidedAction();
      if(DestroyOnCollide)
      {
        GameObject.Destroy(gameObject);
      }
    }
  }

  void OnTriggerEnter2D(Collider2D coll)
  {
    if(coll.gameObject.tag == "Player")
    {
      doTriggeredAction();
      if (DestroyOnTrigger)
      {
        GameObject.Destroy(gameObject);
      }
    }
  }

  protected virtual void doCollidedAction()
  {

  }

  protected virtual void doTriggeredAction()
  {

  }
}
