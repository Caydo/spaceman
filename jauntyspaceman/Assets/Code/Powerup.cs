using UnityEngine;
using UnityEngine.UI;

public class Powerup : PlayerInteractionTrigger
{
  public float OxygenToGain;
  OxygenBarController oxygenController;
  PlayerController playerController;

	private bool activatedTint = false;

  void Start()
  {
    oxygenController = GameObject.FindGameObjectWithTag("OxygenController").GetComponent<OxygenBarController>();
    playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
  }

  protected override void doTriggeredAction()
  {
    oxygenController.GainOxygen(OxygenToGain);
		gameObject.GetComponent<SpriteRenderer>().enabled = false;
		if(!playerController.ShouldAllowJump)
    { 
		  GameObject Colors = GameObject.FindGameObjectWithTag("CrazyColors");
		  Colors.SetActive(true);
		  Colors.GetComponent<Image>().enabled = true;
		  Colors.GetComponent<Animator>().SetTrigger("FadeIn");
		  Colors.GetComponent<RandomTint>().ShouldDoCrazy = true;
      playerController.ShouldAllowJump = true;
		}
  }
}
