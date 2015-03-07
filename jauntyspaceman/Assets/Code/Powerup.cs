using UnityEngine;

public class Powerup : PlayerInteractionTrigger
{
  public float OxygenToGain;
  OxygenBarController oxygenController;

  void Awake()
  {
    oxygenController = GameObject.FindGameObjectWithTag("OxygenController").GetComponent<OxygenBarController>();
  }

  protected override void doAction()
  {
    oxygenController.GainOxygen(OxygenToGain);
  }
}
