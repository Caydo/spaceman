using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextCrawl : MonoBehaviour
{
  public bool CrawlOnStart;
  public string TextToAdd;
  public float TimeBetweenNextCrawl;
  public bool FullySetText = false;
  public Button ButtonToProgress;
  public float DelayBeforeCrawl;
  
  Text textToCrawl;

  void Awake()
  {
    TextToAdd = "The smell of pine does little to ease your fears of this place. The sight of the dead body doesn't really help either. The mission to check out the weird forest's taken a bad turn."
      + "\n\nAlthough, a wizard wielding an enchanted baseball bat should be okay here, right?\n\n Who's that person in front of you though? They don't look familiar. "
     + "\n\nMaybe exploring the forest for clues is a good option? It's late and magic's definitely around here. You don't think you'll be eaten by a grue. What do you do now?\n\nClick To Continue";

    textToCrawl = GetComponent<Text>();
  }

  void Start()
  {
    textToCrawl.text = string.Empty;
    StartCoroutine(addTextThenWait());
  }

  IEnumerator addTextThenWait()
  {
    yield return new WaitForSeconds(DelayBeforeCrawl);
    if (CrawlOnStart)
    {
      for (int i = 0; i < TextToAdd.Length; i++)
      {
        if (!FullySetText)
        {
          textToCrawl.text += TextToAdd[i];
          yield return new WaitForSeconds(TimeBetweenNextCrawl);
        }
        else
        {
          yield break;
        }
      }
    }
    progress();
  }

  void Update()
  {
    if (Input.GetButtonUp("Fire1") || Input.GetButtonUp("Fire2"))
    {
      if (!FullySetText)
      {
        progress();
      }
    }
  }

  void progress()
  {
    FullySetText = true;
    StopCoroutine(addTextThenWait());
    textToCrawl.text = TextToAdd;
    ButtonToProgress.interactable = true;
  }
}