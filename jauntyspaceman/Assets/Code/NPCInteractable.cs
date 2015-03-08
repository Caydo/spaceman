using UnityEngine;
public class NPCInteractable : PlayerInteractionTrigger
{
  public string FollowerName;
  ExpandingItem textPanel;

  protected override void doTriggeredAction()
  {
    textPanel = GameObject.FindGameObjectWithTag("TextPanel").GetComponent<ExpandingItem>();
    textPanel.DoExpand();

    //onTriggerEnterObject.GetComponent<FollowersController>().EnableFollower(FollowerName);
  }
}
