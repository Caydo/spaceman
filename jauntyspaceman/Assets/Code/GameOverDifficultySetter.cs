using UnityEngine;

class GameOverDifficultySetter : MonoBehaviour
{
  public string GameplaySceneName;
  void Awake()
  {
    DontDestroyOnLoad(transform.gameObject);
  }
  
  void OnLevelWasLoaded()
  {
    if(Application.loadedLevelName == GameplaySceneName)
    {
      if (GameObject.FindWithTag("Level").GetComponent<LevelLoader>() != null)
      {
        GameObject.FindWithTag("Level").GetComponent<LevelLoader>().levelDifficulty = 1;
      }
    }
  }
}
