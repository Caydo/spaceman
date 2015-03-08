using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code
{
  class KillPlayerTrigger : PlayerInteractionTrigger
  {
    int time = 0;
    public int TimeToWaitForStuck = 0;
    
    protected override void doTriggerStayAction()
    {
      // while the player is in our collider, increment time
      if (onTriggerStayObject != null)
      {
        time++;
      }

      // assume the player has tried to get out and can't, so kill them
      if (time >= TimeToWaitForStuck)
      {
        onTriggerStayObject.gameObject.GetComponent<PlayerController>().PlayerDead = true;
        GameObject.FindGameObjectWithTag("JetFuelMeter").GetComponent<Slider>().value = 0;
        GameObject.FindGameObjectWithTag("OxygenController").GetComponent<OxygenBarController>().OxygenSlider.value = 0;
        onTriggerStayObject.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
      }
    }
  }
}
