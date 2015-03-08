using System.Collections;
using UnityEngine;

public class DrainingHarmfulElement : PlayerInteractionTrigger
{
  public float OxygenToLose;
  OxygenBarController oxygenController;

  void Awake()
  {
    oxygenController = GameObject.FindGameObjectWithTag("OxygenController").GetComponent<OxygenBarController>();
  }

  protected override void doTriggeredAction()
  {
    StartCoroutine(eachStepDepleteOxygen());
  }

  IEnumerator eachStepDepleteOxygen()
  {
    yield return null;
    oxygenController.LoseOxygen(OxygenToLose);
  }
}
