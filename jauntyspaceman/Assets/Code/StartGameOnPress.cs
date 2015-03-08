using UnityEngine;

public class StartGameOnPress : MonoBehaviour
{
  public Animator OtherText;
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
      myText.SetTrigger("FadeOut");
      OtherText.SetTrigger("FadeOut");
    }
  }

  public void LoadLevel()
  {
    Application.LoadLevel(LevelToLoad);
  }
}
