using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AnswerTimer : MonoBehaviour
{
  public float TimeToAnswer;
  public GameObject TimerGO;
  Slider timerSlider;
  float timeLeft;
  float maxTime;
  LevelLoader levelLoader;
  bool countdownRunning = false;

  void Start()
  {
    timerSlider = TimerGO.GetComponent<Slider>();
    levelLoader = GameObject.FindWithTag("Level").GetComponent<LevelLoader>();
  }

  public void EnableTimer()
  {
    timerSlider.value = 1;
    TimerGO.SetActive(true);
    timeLeft = TimeToAnswer;
    maxTime = TimeToAnswer;

    if(!countdownRunning)
    {
      StartCoroutine(CountdownTime());
    }
  }

  public void DisableTimer()
  {
    timerSlider.value = 0;
    TimerGO.SetActive(false);
    timeLeft = 0;
    maxTime = 0;
    StopCoroutine(CountdownTime());
  }

  IEnumerator CountdownTime()
  {
    countdownRunning = true;
    while(timeLeft >= 0)
    {
      yield return new WaitForSeconds(1.0f);
      timeLeft--;
      timerSlider.value = (timeLeft / maxTime);
    }

    if(levelLoader.npcLoader != null)
    {
      levelLoader.npcLoader.Fail();
    }

    countdownRunning = false;
  }
}
