using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code
{
  class KillPlayerTrigger : PlayerInteractionTrigger
  {
    protected override void doCollidedAction()
    {
      onCollisionObject.gameObject.GetComponent<PlayerController>().PlayerDead = true;
      onCollisionObject.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
      onCollisionObject.gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0;
    }
  }
}
