using UnityEngine;

public class NPCFollower : MonoBehaviour
{
  SpriteRenderer NPCSprite;
  Animator NPCAnimator;
  public GameObject PlayerObject;

  void Awake()
  {
    NPCSprite = GetComponent<SpriteRenderer>();
    NPCAnimator = GetComponent<Animator>();
  }

  void FixedUpdate()
  {
    //transform.position = Vector3.Lerp()
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
