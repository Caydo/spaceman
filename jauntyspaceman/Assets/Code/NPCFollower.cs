using UnityEngine;

public class NPCFollower : PlayerInteractionTrigger
{
  SpriteRenderer NPCSprite;
  Animator NPCAnimator;

  void Awake()
  {
    NPCSprite = GetComponent<SpriteRenderer>();
    NPCAnimator = GetComponent<Animator>();
  }

  protected override void doTriggeredAction()
  {
    onTriggerEnterObject.gameObject.GetComponent<FollowersController>().EnableFollower("jim");
    GameObject.Destroy(gameObject);
  }
  
  void ChangeSprite(Sprite changeSprite)
  {
    NPCSprite.sprite = changeSprite;
  }

  void ChangeAnimator(Animator changeAnimator)
  {
    NPCAnimator = changeAnimator;
  }
}
