using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code
{
  class KillPlayerTrigger : PlayerInteractionTrigger
  {
    protected override void doTriggeredAction()
    {
      onTriggerEnterObject.gameObject.GetComponent<PlayerController>().PlayerDead = true;
      onTriggerEnterObject.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
      onTriggerEnterObject.gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0;
    }

    protected override void doTriggerStayAction()
    {
      onTriggerStayObject.gameObject.GetComponent<PlayerController>().PlayerDead = true;
      onTriggerStayObject.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
      onTriggerStayObject.gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0;
    }
  }
}
