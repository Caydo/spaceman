using UnityEngine;

public class DestroyOnInvisible : MonoBehaviour
{
	private const int SHOW_TILES_X_TO_LEFT_OF_PLAYER = 10;
	public bool destroyOnlyAfterVisible = true;
	public bool respectRespawn = true;
	private bool wasVisible = false;

	public PlayerController player; 
	void Start()
  { 
		player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
	}
	void OnBecameVisible() 
	{
		wasVisible = true;
	}

  void OnBecameInvisible()
  {
		bool respectRespawnOk = true; 
		if(respectRespawn)
    {
      if (player != null)
      {
        if (player.MostRecentSpawnPoint != null)
        {
          respectRespawnOk = (player.MostRecentSpawnPoint.transform.position.x - SHOW_TILES_X_TO_LEFT_OF_PLAYER) > gameObject.transform.position.x;
        }
      }
		}
		if((!respectRespawnOk) || (!destroyOnlyAfterVisible) || wasVisible) { 
    		GameObject.Destroy(gameObject);
		}
  }
}
