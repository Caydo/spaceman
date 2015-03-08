using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ContinualFallObject : PlayerInteractionTrigger
{
  Slider jetFuelMeter;
  void Start()
  {
    jetFuelMeter = GameObject.FindGameObjectWithTag("JetFuelMeter").GetComponent<Slider>();
  }

  protected override void doTriggeredAction()
  {
    onTriggerEnterObject.GetComponent<Animator>().SetTrigger("Fall");
    jetFuelMeter.value = 0;
  }
}
