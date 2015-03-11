using UnityEngine;

public class ClickableCat : Clicky
{
  public SpriteRenderer CatSprite;
  public ParticleSystem CatParticles;
  
  private bool clicked = false; 
  protected override void doAction()
  {
    if(!clicked)
    { 
      // turn off cat sprite
      CatParticles.Play();
	    CatSprite.enabled = false;
      clicked = true;

      GameObject.FindGameObjectWithTag("Catstellation").GetComponent<CatstellationController>().ClickedCat();
    }
  }
}
