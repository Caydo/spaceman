using UnityEngine;

public class ClickableCat : ClickableItem
{
  protected override void doAction()
  {
    // turn off cat sprite
    GetComponent<SpriteRenderer>().enabled = false;
    GetComponent<ParticleSystem>().Play();
  }
}
