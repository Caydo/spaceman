using UnityEngine;
using System.Collections.Generic;
using System.Collections;

class CatstellationController : MonoBehaviour
{
  List<int> selectedCats = new List<int>();
  int catSelected = 0;
  Animator animator;
  public int MaxCats = 3;
	public Transform player; 
  void Start()
  {
    animator = GetComponent<Animator>();
  }

	void Update() 
	{
		gameObject.transform.position = new Vector3(
			player.position.x, 
			gameObject.transform.position.y, 
			gameObject.transform.position.z
		);
	}

  public void ClickedCat()
  {
    if(selectedCats.Count != MaxCats)
    {
      StartCoroutine(getCatNumber());
    }
  }

  IEnumerator getCatNumber()
  {
		catSelected = Random.Range (0, MaxCats) + 1;

    while (selectedCats.Contains(catSelected))
    {
      catSelected = Random.Range(0, MaxCats) + 1;
      yield return null;
    }
		Debug.LogFormat ("Selecting cat number {0} {1} {2}", animator, catSelected, "Star" + catSelected);
    selectedCats.Add(catSelected);
		animator.SetTrigger ("Star" + catSelected);
		animator.SetBool ("Star" + catSelected + " 0", true);
  }
}