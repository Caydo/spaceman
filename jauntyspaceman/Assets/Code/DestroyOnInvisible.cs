using UnityEngine;

public class DestroyOnInvisible : MonoBehaviour
{

	public bool destroyOnlyAfterVisible = true;
	private bool wasVisible = false;

	void OnBecameVisible() 
	{
		wasVisible = true;
	}

  void OnBecameInvisible()
  {
		if((!destroyOnlyAfterVisible) || wasVisible) { 
    		GameObject.Destroy(gameObject);
		}
  }
}