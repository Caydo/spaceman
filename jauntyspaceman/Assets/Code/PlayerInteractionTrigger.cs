using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class PlayerInteractionTrigger : MonoBehaviour
{
  public bool DestroyOnCollide;
  public bool DestroyOnTrigger;
  protected Collision2D onCollisionObject;
  protected Collider2D onTriggerEnterObject;
  protected Collider2D onTriggerStayObject;

  void OnCollisionEnter2D(Collision2D coll)
  {
    if(coll.gameObject.tag == "Player")
    {
      onCollisionObject = coll;
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
      onTriggerEnterObject = coll;
      doTriggeredAction();
      if (DestroyOnTrigger)
      {
        GameObject.Destroy(gameObject);
      }
    }
  }

  void OnTriggerStay2D(Collider2D other)
  {
    if(other.gameObject.tag == "Player")
    {
      onTriggerStayObject = other;
      doTriggerStayAction();
    }
  }


  protected virtual void doCollidedAction()
  {

  }

  protected virtual void doTriggeredAction()
  {

  }

  protected virtual void doTriggerStayAction()
  {

  }
}
