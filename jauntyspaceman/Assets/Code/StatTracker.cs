using UnityEngine;

public class StatTracker : MonoBehaviour
{
  public int PowerupsStat;
  public int NPCsStat;
  public int CatsStat;
  public int RespawnsStat;
  public int DeathsStat;
  public int OxygenUnitsStat;

  void Awake()
  {
    DontDestroyOnLoad(transform.gameObject);
  }

  public int GetStat(string statName)
  {
    int result = 0;
    switch(statName)
    {
      case "powerups":
        result = PowerupsStat;
      break;
      case "npcs":
        result = NPCsStat;
      break;
      case "cats":
        result = CatsStat;
      break;
      case "respawns":
        result = RespawnsStat;
      break;
      case "death":
        result = DeathsStat;
      break;
      case "oxygen":
        result = OxygenUnitsStat;
      break;
      default:
        Debug.LogError("couldn't find a stat for: " + name);
      break;
    }

    return result;
  }
}