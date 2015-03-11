using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code
{
  class KillPlayerTrigger : PlayerInteractionTrigger
  {
    float time = 0;
    public float TimeToWaitForStuck = 0;

    protected override void doTriggerStayAction()
    {
      StartCoroutine(waitThenUptickTime());
    }

    IEnumerator waitThenUptickTime()
    {
      // while the player is in our collider, increment time
      if(onTriggerStayObject != null)
      {
        // wait a frame then uptick
        yield return null;
        time += 0.1f;
      }
      
      // assume the player has tried to get out and can't, so kill them
      if (time >= TimeToWaitForStuck)
      {
        var player = onTriggerStayObject.gameObject.GetComponent<PlayerController>();
        if(!player.animator.GetBool ("Jump"))
        {
          time = 0;
          player.PlayerDead = true;
          GameObject.FindGameObjectWithTag("JetFuelMeter").GetComponent<Slider>().value = 0;
          onTriggerStayObject.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        }
      }
    }
  }
}
