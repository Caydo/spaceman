using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextCrawl : MonoBehaviour
{
  public bool CrawlOnStart;
  public string TextToAdd;
  public string AdditionalText;
  public float TimeBetweenNextCrawl;
  public bool FullySetText = false;
  public float DelayBeforeCrawl;
  public bool AllowClickSkip;
  public bool active = false;
  public Text textToCrawl;
  public Text textToDisplayFast;

  AnswerTimer answerTimer;

  void Awake()
  {
//    TextToAdd = "The smell of pine does little to ease your fears of this place. The sight of the dead body doesn't really help either. The mission to check out the weird forest's taken a bad turn."
//      + "\n\nAlthough, a wizard wielding an enchanted baseball bat should be okay here, right?\n\n Who's that person in front of you though? They don't look familiar. "
//     + "\n\nMaybe exploring the forest for clues is a good option? It's late and magic's definitely around here. You don't think you'll be eaten by a grue. What do you do now?\n\nClick To Continue";

    //textToCrawl = GetComponent<Text>();
  }

  void Start()
  {
    answerTimer = GameObject.FindGameObjectWithTag("AnswerTimer").GetComponent<AnswerTimer>();
  }

	public void Reset() 
	{
		Debug.Log ("reset");
		TextToAdd = "";
		AdditionalText = "";
		FullySetText = false;
		textToDisplayFast.text = string.Empty;
		textToCrawl.text = string.Empty;	
	}

	public void StartCrawl() 
	{
		Debug.Log ("start crawl [" + TextToAdd + "]");
		active = true;
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
    if(AllowClickSkip && Input.GetButtonUp("Fire1") || Input.GetButtonUp("Fire2"))
    {
      if (!FullySetText)
      {
        progress();
      }
    }
  }

  void progress()
  {
		Debug.Log ("finished");
    FullySetText = true;
    StopCoroutine(addTextThenWait());
		textToCrawl.text = TextToAdd ;
    textToDisplayFast.text = AdditionalText;
		active = false;

    answerTimer.EnableTimer();
  }
}
