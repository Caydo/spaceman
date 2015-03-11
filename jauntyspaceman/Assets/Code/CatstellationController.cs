using UnityEngine;
using System.Collections.Generic;
using System.Collections;

class CatstellationController : MonoBehaviour
{
  List<int> selectedCats = new List<int>();
  int catSelected = 0;
  Animator animator;
  public int MaxCats = 8;

  void Start()
  {
    animator = GetComponent<Animator>();
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
    catSelected = Random.Range(0, MaxCats);

    while (selectedCats.Contains(catSelected))
    {
      catSelected = Random.Range(1, MaxCats);
      yield return null;
    }

    selectedCats.Add(catSelected);
    animator.SetInteger("SelectedCatNumber", catSelected);
  }
}