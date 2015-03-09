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
      GameObject.FindWithTag("Level").GetComponent<LevelLoader>().levelDifficulty = 1;
    }
  }
}
