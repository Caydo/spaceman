using UnityEngine;

public class NPCFollower : MonoBehaviour
{
  SpriteRenderer NPCSprite;
  Animator NPCAnimator;

  void Awake()
  {
    NPCSprite = GetComponent<SpriteRenderer>();
    NPCAnimator = GetComponent<Animator>();
  }

  public void ChangeSprite(Sprite changeSprite)
  {
    NPCSprite.sprite = changeSprite;
  }

  public void ChangeAnimator(Animator changeAnimator)
  {
    NPCAnimator = changeAnimator;
  }
}
