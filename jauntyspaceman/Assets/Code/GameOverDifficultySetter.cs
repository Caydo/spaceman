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
      if (GameObject.FindWithTag("LevelLoader").GetComponent<LevelLoader>() != null)
      {
        LevelLoader loader = GameObject.FindWithTag("LevelLoader").GetComponent<LevelLoader>();
        loader.gameOverCallback();
      }
    }
  }
}
