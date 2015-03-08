using UnityEngine;
public class SetRespawnPoint : PlayerInteractionTrigger
{
  protected override void doTriggeredAction()
  {
    onTriggerEnterObject.GetComponent<PlayerController>().MostRecentSpawnPoint = transform;
  }
}
