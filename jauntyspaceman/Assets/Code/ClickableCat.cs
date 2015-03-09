using UnityEngine;

public class ClickableCat : Clicky
{
  public SpriteRenderer CatSprite;
  public ParticleSystem CatParticles;
  protected override void doAction()
  {
    // turn off cat sprite
    CatParticles.Play();
    CatSprite.enabled = false;
  }
}
