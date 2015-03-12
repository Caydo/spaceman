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
    PlayerController player;
    bool triggerCollider = false;
    bool otherCollider = false;

    protected override void doCollisionStayAction()
    {
      otherCollider = true;
      StartCoroutine(waitThenUptickTime());
    }

    protected override void doTriggerStayAction()
    {
      triggerCollider = true;
      StartCoroutine(waitThenUptickTime());
    }

    IEnumerator waitThenUptickTime()
    {
      if(triggerCollider)
      {
        // while the player is in our collider, increment time
        if(onTriggerStayObject.gameObject != null)
        {
          // wait a frame then uptick
          yield return null;
          time += 0.1f;
        }
      }

      if(otherCollider)
      {
        // while the player is in our collider, increment time
        if(onCollisionStayObject.gameObject != null)
        {
          // wait a frame then uptick
          yield return null;
          time += 0.1f;
        }
      }

      player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

      // assume the player has tried to get out and can't, so kill them
      if (time >= TimeToWaitForStuck)
      {
        if(!player.animator.GetBool ("Jump"))
        {
          time = 0;
          player.PlayerDead = true;
          GameObject.FindGameObjectWithTag("JetFuelMeter").GetComponent<Slider>().value = 0;
          player.TurnOffColliders();
        }
      }
    }
  }
}
