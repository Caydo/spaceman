class TopScreenDontRespawn : PlayerInteractionTrigger
{
  protected override void doTriggeredAction()
  {
    onTriggerEnterObject.GetComponent<PlayerController>().ShouldRespawn = false;
  }

  protected override void doTriggerExitAction()
  {
    onTriggerEnterObject.GetComponent<PlayerController>().ShouldRespawn = true;
  }
}
