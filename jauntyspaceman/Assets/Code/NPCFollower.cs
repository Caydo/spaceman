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
    Sprite followerSprite = gameObject.GetComponent<NpcEncounterLoader>().FollowerSprite;
    onTriggerEnterObject.gameObject.GetComponent<FollowersController>().EnableFollower("jim", followerSprite);
    //gameObject.GetComponent<SpriteRenderer>().enabled = false;
	  gameObject.GetComponent<SpriteRenderer>().sprite = null;

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
