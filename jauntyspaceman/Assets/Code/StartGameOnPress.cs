using UnityEngine;

public class StartGameOnPress : MonoBehaviour
{
  public Animator[] TextsToFadeout;
  public string LevelToLoad;
  Animator myText;

  void Awake()
  {
    myText = GetComponent<Animator>();
  }

  void Update()
  {
    if(Input.anyKey && Input.inputString != string.Empty)
    {
      foreach(Animator anim in TextsToFadeout)
      {
        anim.SetTrigger("FadeOut");
      }
    }
  }

  public void LoadLevel()
  {
    Application.LoadLevel(LevelToLoad);
  }
}
