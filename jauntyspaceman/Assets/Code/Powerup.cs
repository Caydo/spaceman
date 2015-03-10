using UnityEngine;
using UnityEngine.UI;

public class Powerup : PlayerInteractionTrigger
{
  public float OxygenToGain;
  OxygenBarController oxygenController;

	private bool activatedTint = false;

  void Awake()
  {
    oxygenController = GameObject.FindGameObjectWithTag("OxygenController").GetComponent<OxygenBarController>();
  }

  protected override void doTriggeredAction()
  {
    oxygenController.GainOxygen(OxygenToGain);
		gameObject.GetComponent<SpriteRenderer>().enabled = false;
		if(!activatedTint) { 
		    GameObject Colors = GameObject.FindGameObjectWithTag("CrazyColors");
		    Colors.SetActive(true);
		    Colors.GetComponent<Image>().enabled = true;
		    Colors.GetComponent<Animator>().SetTrigger("FadeIn");
		    Colors.GetComponent<RandomTint>().ShouldDoCrazy = true;
		}
  }
}
