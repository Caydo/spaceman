using UnityEngine;

public class ClickableCat : Clicky
{
  public SpriteRenderer CatSprite;
  public ParticleSystem CatParticles;
  
  private bool clicked = false;
  StatTracker statTracker;

  void Start()
  {
    statTracker = GameObject.FindGameObjectWithTag("StatTracker").GetComponent<StatTracker>();
  }

  protected override void doAction()
  {
    if(!clicked)
    { 
      // turn off cat sprite
      CatParticles.Play();
	    CatSprite.enabled = false;
      clicked = true;

      statTracker.CatsStat++;
      GameObject.FindGameObjectWithTag("Catstellation").GetComponent<CatstellationController>().ClickedCat();
    }
  }
}
