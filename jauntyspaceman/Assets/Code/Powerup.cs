using UnityEngine;
using UnityEngine.UI;

public class Powerup : PlayerInteractionTrigger
{
  public float OxygenToGain;
  OxygenBarController oxygenController;
  PlayerController playerController;
  RandomTint tint;
  StatTracker statTracker;

  void Start()
  {
    oxygenController = GameObject.FindGameObjectWithTag("OxygenController").GetComponent<OxygenBarController>();
    playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    tint = GameObject.FindGameObjectWithTag("CrazyColors").GetComponent<RandomTint>();
    statTracker = GameObject.FindWithTag("StatTracker").GetComponent<StatTracker>();
  }

  protected override void doTriggeredAction()
  {
    oxygenController.GainOxygen(OxygenToGain);
		gameObject.GetComponent<SpriteRenderer>().enabled = false;
    statTracker.PowerupsStat++;
    if(!tint.ShouldDoCrazy)
    {
      tint.ShouldDoCrazy = true;
      tint.gameObject.GetComponent<Animator>().SetTrigger("FadeIn");
      playerController.ShouldAllowJump = true;
    }
  }
}
