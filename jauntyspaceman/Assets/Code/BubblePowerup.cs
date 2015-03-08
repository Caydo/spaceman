using UnityEngine;

public class BubblePowerup : PlayerInteractionTrigger
{
  public float OxygenToGain;
  OxygenBarController oxygenController;

  void Awake()
  {
    oxygenController = GameObject.FindGameObjectWithTag("OxygenController").GetComponent<OxygenBarController>();
  }

  protected override void doCollidedAction()
  {
    oxygenController.GainOxygen(OxygenToGain);
  }
}
