using UnityEngine;

public class HarmfulElement : PlayerInteractionTrigger
{
  public float OxygenToLose;
  OxygenBarController oxygenController;

  void Awake()
  {
    oxygenController = GameObject.FindGameObjectWithTag("OxygenController").GetComponent<OxygenBarController>();
  }

  protected override void doCollidedAction()
  {
    oxygenController.LoseOxygen(OxygenToLose);
  }
}
