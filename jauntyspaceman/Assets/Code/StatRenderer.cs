using UnityEngine;
using UnityEngine.UI;

public class StatRenderer : MonoBehaviour
{
  Text textLabel;
  StatTracker statTracker;
  public string StatName;
  public string StatLookName;

  void Start()
  {
    statTracker = GameObject.FindWithTag("StatTracker").GetComponent<StatTracker>();
    textLabel = GetComponent<Text>();
    textLabel.text = string.Format("{0}: {1}", StatName, statTracker.GetStat(StatLookName));
  }
}