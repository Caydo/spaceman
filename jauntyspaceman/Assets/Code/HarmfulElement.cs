using UnityEngine;

public class HarmfulElement : PlayerInteractionTrigger
{
  public float OxygenToGain;
  OxygenBarController oxygenController;

  void Awake()
  {
    oxygenController = GameObject.FindGameObjectWithTag("OxygenController").GetComponent<OxygenBarController>();
  }

  protected override void doAction()
  {
    oxygenController.LoseOxygen(OxygenToGain);
  }
}
