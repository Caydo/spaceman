using UnityEngine;

public class ClickableCat : Clicky
{
  public ParticleSystem CatParticles;
  protected override void doAction()
  {
    // turn off cat sprite
    CatParticles.Play();
    GetComponent<SpriteRenderer>().enabled = false;
  }
}
