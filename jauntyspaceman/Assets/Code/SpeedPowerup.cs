using UnityEngine;

public class SpeedPowerup : PlayerInteractionTrigger
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
