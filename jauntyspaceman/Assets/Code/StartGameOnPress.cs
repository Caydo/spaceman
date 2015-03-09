using UnityEngine;

public class StartGameOnPress : MonoBehaviour
{
  public Animator OtherText;
  public Animator OtherOtherText;
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
      if(OtherOtherText != null)
      {
        OtherOtherText.SetTrigger("FadeOut");
      }
    }
  }

  public void LoadLevel()
  {
    Application.LoadLevel(LevelToLoad);
  }
}
